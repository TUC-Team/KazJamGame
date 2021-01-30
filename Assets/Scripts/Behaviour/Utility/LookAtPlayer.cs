using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private PlayerCharacterController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerCharacterController>();
    }

    private void LateUpdate()
    {
        transform.LookAt(player.transform.position);
    }
}
