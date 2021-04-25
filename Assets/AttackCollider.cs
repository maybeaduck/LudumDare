using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyActor.Target = null;
        }
    }
}
