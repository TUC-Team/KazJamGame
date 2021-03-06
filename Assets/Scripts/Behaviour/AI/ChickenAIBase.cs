﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ChickenAIBase : MonoBehaviour
{
    static readonly int Move = Animator.StringToHash("Move");

    public bool IsKilled { get; private set; } = false;
    public UnityEvent diedEvent;
    public AK.Wwise.Event chic_death;

    [SerializeField] private float stayingTime = 2f;
    [SerializeField] private float targetStoppingDistance = .5f;

    [SerializeField] Animator _animator;

    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private float timeInTargetPosition;
    private ChickenManager chickenManager;
#pragma warning disable 108,114
    private Collider collider;
#pragma warning restore 108,114

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        collider = GetComponent<Collider>();
        chickenManager = FindObjectOfType<ChickenManager>();

        if ( chickenManager ) {
            chickenManager.RegisterChicken(this);
        }

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

        if ( chickenManager ) {
            chickenManager.RemoveChicken(this);
        }
        chic_death.Post(gameObject);

        diedEvent?.Invoke();

        transform.localScale = new Vector3(1, .05f, 1);

        agent.enabled = false;
        collider.enabled = false;
        this.enabled = false;

        _animator.SetBool(Move, false);
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
        _animator.SetBool(Move, true);
    }
}
