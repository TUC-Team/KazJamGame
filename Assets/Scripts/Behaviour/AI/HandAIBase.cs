using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HandAIBase : MonoBehaviour
{
    [SerializeField] private HandState startState;
    [SerializeField] private float attackRate = 2f;
    
    private NavMeshAgent agent;
    private ChickenAIBase targetChicken;
    private ChickenAIBase chickenInAttackRange;
    private Animator animator;
    private List<ChickenAIBase> chickens = new List<ChickenAIBase>();
    private HandState state;
    private float timeSinceLastAttack = 0;

    private void Awake()
    {
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
        timeSinceLastAttack += Time.deltaTime;
            
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

    public void RegisterChicken(ChickenAIBase chicken)
    {
        chickens.Add(chicken);
    }

    public void RemoveChicken(ChickenAIBase chicken)
    {
        chickens.Remove(chicken);

        if (chicken == chickenInAttackRange)
            chickenInAttackRange = null;
    }

    private ChickenAIBase FindNearestChicken()
    {
        float minDist = Mathf.Infinity;
        ChickenAIBase nearestChicken = null;
        
        foreach (var chicken in chickens)
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

        if (targetChicken == null)
        {
            print("Все курицы закончились");
            return;
        }
        
        agent.SetDestination(targetChicken.transform.position);

        if (chickenInAttackRange != null && timeSinceLastAttack >= attackRate)
        {
            PlayAttackAnim();
            timeSinceLastAttack = 0;
        }
    }
    
    private void Patrol(){}

    private void OnTriggerStay(Collider other)
    {
        var chicken = other.GetComponent<ChickenAIBase>();

        if (chicken == null)
            return;

        if (chicken.IsKilled)
            return;

        chickenInAttackRange = chicken;
    }

    private void OnAttackAnimation()
    {
        chickenInAttackRange.Kill();
        chickenInAttackRange = null;
    }
}

enum HandState
{
    Hunt, Patrol, Appearing
}
