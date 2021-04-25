
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using Zlodey;


public class PlayerCharacter : MonoBehaviour
{
    public PersonActor playerPerson;

    private IEnumerator Start()
    {
        yield return null;
        playerPerson.ThisEntity.Get<PlayerData>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            var actorStatsComponent = other.GetComponent<AttackHand>().actor.actor.StatsComponent;
            playerPerson.ThisEntity.Get<DamageEvent>().Value = Random.Range(actorStatsComponent.Damage.MinValue,
                actorStatsComponent.Damage.MaxValue);
        }
        
    }
}

internal struct PlayerData
{
    public Vector3 Direction;
}
