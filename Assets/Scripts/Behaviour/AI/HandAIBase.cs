using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = System.Random;

public class HandAIBase : MonoBehaviour
{
    public UnityEvent DiedEvent;
    
    [SerializeField] private HandState startState;
    [SerializeField] private float minPatrolTime = 5f;
    [SerializeField] private float maxPatrolTime = 10f;
    [SerializeField] private float stayTimeInPatrolPoint = 2f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float healingSpeed = 10f;

    [SerializeField] FingerHolder _fingerHolder;

    private NavMeshAgent _agent;
    private ChickenAIBase _targetChicken;
    private List<ChickenAIBase> _chickensInAttackRange = new List<ChickenAIBase>();
    private Animator _animator;
    private HandState _state;
    private float _currentPatrolTime = 0;
    private float _patrolTime = 0;
    private float _currentStayTimeInPatrolPoint = 0;
    private float _timeSinceLastAttack = 0;
    private int _deathCount = 0;
    private Vector3 _targetPatrolPosition;   
    private ChickenManager _chickenManager;
    private Health _health;
    private GameController _gameController;

    private void Awake()
    {
        _chickenManager = FindObjectOfType<ChickenManager>();
        _gameController = FindObjectOfType<GameController>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        
        if (_health == null)
            _health = GetComponentInChildren<Health>();
    }

    private void OnEnable()
    {
        _health.onDie += OnDie;
    }
    
    private void OnDisable()
    {
        _health.onDie -= OnDie;
    }

    private void Start()
    {
        _targetChicken = FindNearestChicken();

        ChangeState(startState);
    }

    private void Update()
    {
        if (_gameController.IsGameEnded)
            return;
        
        switch (_state)
        {
            case HandState.Hunt:
                HuntUpdate();
                break;
            case HandState.Patrol:
                PatrolUpdate();
                break;
            case HandState.Healing:
                HealingUpdate();
                break;
        }
    }

    private ChickenAIBase FindNearestChicken()
    {
        float minDist = Mathf.Infinity;
        ChickenAIBase nearestChicken = null;
        
        foreach (var chicken in _chickenManager.Chickens)
        {
            if (chicken.IsKilled)
                continue;
            
            float dist = Vector3.Distance(transform.position, chicken.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestChicken = chicken;
            }
        }

        return nearestChicken;
    }

    private void PlayAttackAnim()
    {
        _animator.SetTrigger("attack");
    }

    private void OnDie()
    {
        _deathCount++;

        if ( !_fingerHolder.TryRemoveFinger() )
        {
            _gameController.EndGame(true);
            DiedEvent?.Invoke();
            return;
        }
        
        ChangeState(HandState.Healing);
    }

    private void HuntUpdate()
    {
        _targetChicken = FindNearestChicken();

        _timeSinceLastAttack += Time.deltaTime;

        if (_targetChicken == null)
        {
            ChangeState(HandState.Patrol);
            return;
        }
        
        _agent.SetDestination(_targetChicken.transform.position);

        if (_chickensInAttackRange.Count > 0 && _timeSinceLastAttack >= attackRate)
        {
            PlayAttackAnim();
            _timeSinceLastAttack = 0;
        }
    }
    
    private void HuntStart(){}
    
    private void PatrolStart(){}

    private void HealingStart()
    {
        _animator.SetTrigger("healing");
        _health.Resurrect();
        _health.invincible = true;
    }

    private void AppearingStart()
    {
        _animator.SetTrigger("appear");

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(2.5f);

            ChangeState(HandState.Hunt);
            _health.invincible = false;
        }

        StartCoroutine(Delay());
    }

    private void PatrolUpdate()
    {
        if (_patrolTime == 0)
        {
            _patrolTime = UnityEngine.Random.Range(minPatrolTime, maxPatrolTime);
            ChangeTargetRandom();
        }

        _agent.SetDestination(_targetPatrolPosition);

        if (Vector3.Distance(transform.position, _targetPatrolPosition) < .2f)
            _currentStayTimeInPatrolPoint += Time.deltaTime;

        if (_currentStayTimeInPatrolPoint >= stayTimeInPatrolPoint)
        {
            _currentStayTimeInPatrolPoint = 0;
            ChangeTargetRandom();
        }
        
        _currentPatrolTime += Time.deltaTime;

        if (_currentPatrolTime >= _patrolTime)
        {
            _patrolTime = 0;
            _currentPatrolTime = 0;
            ChangeState(HandState.Hunt);
        }
    }

    private void HealingUpdate()
    {
        _health.Heal(Time.deltaTime * healingSpeed);
        
        if (Math.Abs(_health.currentHealth - _health.maxHealth) <= Mathf.Epsilon)
            ChangeState(HandState.Appearing);
    }

    private void ChangeState(HandState newState)
    {
        _state = newState;
        
        switch (newState)
        {
            case HandState.Hunt:
                HuntStart();
                break;
            case HandState.Healing:
                HealingStart();
                break;
            case HandState.Appearing:
                AppearingStart();
                break;
            case HandState.Patrol:
                PatrolStart();
                break;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var chicken = other.GetComponent<ChickenAIBase>();

        if (chicken == null)
            return;

        if (chicken.IsKilled)
            return;

        _chickensInAttackRange.Add(chicken);
    }

    private void OnTriggerExit(Collider other)
    {
        var chicken = other.GetComponent<ChickenAIBase>();

        if (chicken == null)
            return;
        
        _chickensInAttackRange.Remove(chicken);
    }

    private void OnAttackAnimation()
    {
        if (_chickensInAttackRange.Count == 0)
        {
            _state = HandState.Patrol;
            return;
        }
        
        _chickensInAttackRange[0].Kill();
        _chickensInAttackRange.RemoveAt(0);
        
        _state = HandState.Patrol;
    }
    
    private void ChangeTargetRandom()
    {
        var position = transform.position;
        Vector3 target = position;

        float x = UnityEngine.Random.Range(-15f, 15f);
        float z = UnityEngine.Random.Range(-15f, 15f);

        target.x += x;
        target.y = position.y;
        target.z += z;

        _currentStayTimeInPatrolPoint = 0;
        
        _targetPatrolPosition = target;
    }
}

enum HandState
{
    Hunt, Patrol, Appearing, Healing
}
