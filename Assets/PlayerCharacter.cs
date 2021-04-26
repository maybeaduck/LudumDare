
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;
using Zlodey;


public class PlayerCharacter : MonoBehaviour
{
    public PersonActor playerPerson;
    public Transform WeaponPosition;
    public GameObject WeaponContainer;
    private Transform weaponTransform;
    public WeaponCollectActor LastCollectWeapon;
    public StaticData _static;
    private IEnumerator Start()
    {
        yield return null;
        playerPerson.ThisEntity.Get<PlayerData>();
        _static = Service<StaticData>.Get();
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
        if (other.CompareTag("Weapon"))
        {
            var weaponActor = other.GetComponent<WeaponCollectActor>();
            if (weaponActor.itsPickUp == false)
            {
                var dropWeapon = Instantiate(LastCollectWeapon, transform.position, Quaternion.identity);
                dropWeapon.WeaponScript = playerPerson.Weapon;
                LastCollectWeapon = _static.allWeapons.Find(actor => actor.name.Contains(weaponActor.name));
                weaponActor.Animator.SetBool("PickUp",true);
                var weaponScript = Instantiate(weaponActor.WeaponScript, playerPerson.transform);
                playerPerson.Weapon.gameObject.SetActive(false);
                playerPerson.Weapon = weaponScript;
                var model = Instantiate(weaponActor.WeaponModel, WeaponPosition);
                weaponTransform = WeaponContainer.transform;
                WeaponContainer.SetActive(false);
                WeaponContainer = model;
                model.transform.rotation = weaponTransform.rotation;
                model.transform.position = weaponTransform.position;
                model.transform.localScale = weaponTransform.localScale;
                weaponActor.itsPickUp = true;
                playerPerson.ThisEntity.Get<PersonData>().Weapon = playerPerson.Weapon;
                
                
            }
            
        }
    }
}

internal struct PlayerData
{
    public Vector3 Direction;
}
