using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using Zlodey;

public class AtackInput : MonoBehaviour
{
    public EnemyActor actor;
    public void Damage()
    {
        if (actor.Target)
        {
            Debug.Log("Punch");
            var maxDamageValue = actor.actor.StatsComponent.Damage.MaxValue;
            var minDamageValue = actor.actor.StatsComponent.Damage.MaxValue;
            actor.Target.GetComponent<PersonActor>().ThisEntity.Get<DamageEvent>().Value =
                Random.Range(minDamageValue,maxDamageValue);
        }
        
    }
}
