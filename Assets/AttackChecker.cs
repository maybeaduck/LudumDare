using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;
using Zlodey;

public class AttackChecker : MonoBehaviour
{
    public AttackHand[] ActorHand;
    public PersonActor Actor;
    public void ColliderEnable()
    {
        foreach (var item in ActorHand)
        {
            item.gameObject.SetActive(true);
        }
    }
    public void ColliderDisable()
    {
        foreach (var item in ActorHand)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void Stop()
    {
        Actor.ThisEntity.Get<Stop>();
    }

    public void Resume()
    {
        Actor.ThisEntity.Get<Return>();
    }
    
}
