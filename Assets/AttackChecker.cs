using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

public class AttackChecker : MonoBehaviour
{

    public PersonActor PersonActor;
    public EnemyActor EnemyActor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PersonActor.Animator.SetTrigger("Punch");
            EnemyActor.Target = other;
        }
    }
}
