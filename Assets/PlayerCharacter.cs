
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

        if (other.CompareTag("Heall"))
        {
            if (playerPerson.StatsComponent.Health.Value < 100f)
            {
                var bandageActor = other.GetComponent<BandageActor>();
                bandageActor.Animator.SetBool("PickUp", true);
                playerPerson.ThisEntity.Get<DamageEvent>().Value = bandageActor.HeallValue * -1;
                bandageActor.HeallValue = 0;
            }
            
        }

        if (other.CompareTag("Ammo"))
        {
            var ammoActor = other.GetComponent<AmmoActor>();
            ammoActor.Animator.SetBool("PickUp",true);
            playerPerson.Weapon.AllAmunitionInInvent += playerPerson.Weapon.defaultAmunition* ammoActor.CountAmmoPack;
            ammoActor.CountAmmoPack = 0;
        }
    }
}

internal struct PlayerData
{
    public Vector3 Direction;
}
