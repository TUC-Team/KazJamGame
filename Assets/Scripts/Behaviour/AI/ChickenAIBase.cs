using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ChickenAIBase : MonoBehaviour
{
    public bool IsKilled { get; private set; } = false;
    
    [SerializeField] private float stayingTime = 2f;
    [SerializeField] private float targetStoppingDistance = .5f;
    
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private float timeInTargetPosition;
    private HandAIBase hand;
    private new Collider collider;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        collider = GetComponent<Collider>();
        hand = FindObjectOfType<HandAIBase>();
        
        hand.RegisterChicken(this);

        timeInTargetPosition = 0;
    }

    private void Start()
    {
        targetPosition = transform.position;
        ChangeTargetRandom();
    }

    private void Update()
    {
        agent.SetDestination(targetPosition);

        if (Mathf.Abs(Vector3.Distance(transform.position, targetPosition)) <= targetStoppingDistance)
        {
            timeInTargetPosition += Time.deltaTime;
        }

        if (timeInTargetPosition >= stayingTime)
        {
            ChangeTargetRandom();
        }
    }

    public void Kill()
    {
        IsKilled = true;
        
        hand.RemoveChicken(this);
        
        // play anim ...
        print("chicken " + gameObject.name + " was killed");

        transform.localScale = new Vector3(1, .05f, 1);

        agent.enabled = false;
        collider.enabled = false;
        this.enabled = false;
    }

    private void ChangeTargetRandom()
    {
        var position = transform.position;
        Vector3 target = position;

        float x = Random.Range(-4f, 4f);
        float z = Random.Range(-4f, 4f);

        target.x += x;
        target.y = position.y;
        target.z += z;

        timeInTargetPosition = 0;
        
        targetPosition = target;
    }
}
