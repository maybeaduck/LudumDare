using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

public class AttackChecker : MonoBehaviour
{
    public AttackHand[] ActorHand;

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
    
}
