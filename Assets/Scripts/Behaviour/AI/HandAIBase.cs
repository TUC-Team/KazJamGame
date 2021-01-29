using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class HandAIBase : MonoBehaviour
{
    [SerializeField] private HandState startState;
    [SerializeField] private float minPatrolTime = 5f;
    [SerializeField] private float maxPatrolTime = 10f;
    [SerializeField] private float stayTimeInPatrolPoint = 2f;
    [SerializeField] private float attackRate = 2f;

    private NavMeshAgent agent;
    private ChickenAIBase targetChicken;
    private List<ChickenAIBase> chickensInAttackRange = new List<ChickenAIBase>();
    private Animator animator;
    private HandState state;
    private float currentPatrolTime = 0;
    private float patrolTime = 0;
    private float currentStayTimeInPatrolPoint = 0;
    private float timeSinceLastAttack = 0;
    private Vector3 targetPatrolPosition;
    private ChickenManager chickenManager;

    private void Awake()
    {
        chickenManager = FindObjectOfType<ChickenManager>();   
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private IEnumerator Start()
    {
        targetChicken = FindNearestChicken();
        animator.SetTrigger("appear");

        state = startState;

        // Ждем пока рука не спустится и переводим ее в состояние охоты
        yield return new WaitForSeconds(2.5f);

        state = HandState.Hunt;
    }

    private void Update()
    {
        switch (state)
        {
            case HandState.Hunt:
                Hunt();
                break;
            case HandState.Patrol:
                Patrol();
                break;
        }
    }

    private ChickenAIBase FindNearestChicken()
    {
        float minDist = Mathf.Infinity;
        ChickenAIBase nearestChicken = null;
        
        foreach (var chicken in chickenManager.Chickens)
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
        animator.SetTrigger("attack");
    }

    private void Hunt()
    {
        targetChicken = FindNearestChicken();

        timeSinceLastAttack += Time.deltaTime;

        if (targetChicken == null)
        {
            print("Все курицы закончились");
            state = HandState.Patrol;
            return;
        }
        
        agent.SetDestination(targetChicken.transform.position);

        if (chickensInAttackRange.Count > 0 && timeSinceLastAttack >= attackRate)
        {
            PlayAttackAnim();
            timeSinceLastAttack = 0;
        }
    }

    private void Patrol()
    {
        if (patrolTime == 0)
        {
            patrolTime = UnityEngine.Random.Range(minPatrolTime, maxPatrolTime);
            ChangeTargetRandom();
        }

        agent.SetDestination(targetPatrolPosition);

        if (Vector3.Distance(transform.position, targetPatrolPosition) < .2f)
            currentStayTimeInPatrolPoint += Time.deltaTime;

        if (currentStayTimeInPatrolPoint >= stayTimeInPatrolPoint)
        {
            currentStayTimeInPatrolPoint = 0;
            ChangeTargetRandom();
        }
        
        currentPatrolTime += Time.deltaTime;

        if (currentPatrolTime >= patrolTime)
        {
            patrolTime = 0;
            currentPatrolTime = 0;
            state = HandState.Hunt;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var chicken = other.GetComponent<ChickenAIBase>();

        if (chicken == null)
            return;

        if (chicken.IsKilled)
            return;

        chickensInAttackRange.Add(chicken);
    }

    private void OnTriggerExit(Collider other)
    {
        var chicken = other.GetComponent<ChickenAIBase>();

        if (chicken == null)
            return;
        
        chickensInAttackRange.Remove(chicken);
    }

    private void OnAttackAnimation()
    {
        if (chickensInAttackRange.Count == 0)
        {
            state = HandState.Patrol;
            return;
        }
        
        chickensInAttackRange[0].Kill();
        chickensInAttackRange.RemoveAt(0);
        
        state = HandState.Patrol;
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

        currentStayTimeInPatrolPoint = 0;
        
        targetPatrolPosition = target;
    }
}

enum HandState
{
    Hunt, Patrol, Appearing
}
