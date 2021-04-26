
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;
using Zlodey;
using Random = UnityEngine.Random;


public class PlayerCharacter : MonoBehaviour
{
    public PersonActor playerPerson;
    public Transform WeaponPosition;
    public GameObject WeaponContainer;
    private Transform weaponTransform;
    public WeaponCollectActor LastCollectWeapon;
    public StaticData _static;
    public RuntimeData _runtime;
    public Collider _LastDropWeapon;
    private IEnumerator Start()
    {
        yield return null;
        playerPerson.ThisEntity.Get<PlayerData>();
        _static = Service<StaticData>.Get();
        _runtime = Service<RuntimeData>.Get();
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
                _runtime.AudioSource.PlayOneShot(_static.CollectBandageAudio,0.7f); //audio

                var bandageActor = other.GetComponent<BandageActor>();
                bandageActor.Animator.SetBool("PickUp", true);
                playerPerson.ThisEntity.Get<DamageEvent>().Value = bandageActor.HeallValue * -1;
                bandageActor.HeallValue = 0;
            }
            
        }

        if (other.CompareTag("Ammo"))
        {
            _runtime.AudioSource.PlayOneShot(_static.CollectAmmoAudio, 0.7f); //audio

            var ammoActor = other.GetComponent<AmmoActor>();
            ammoActor.Animator.SetBool("PickUp",true);
            playerPerson.Weapon.AllAmunitionInInvent += playerPerson.Weapon.defaultAmunition* ammoActor.CountAmmoPack;
            ammoActor.CountAmmoPack = 0;
        }
        if (other.CompareTag("Weapon"))
        {
            _runtime.AudioSource.PlayOneShot(_static.CollectWeaponAudio, 0.5f); //audio

            var weaponActor = other.GetComponent<WeaponCollectActor>();
            if (weaponActor.itsPickUp == false)
            {
                weaponActor.ThisEntity.Del<DropData>();
                var dropWeapon = Instantiate(LastCollectWeapon.gameObject, transform.position+Vector3.up*0.5f, Quaternion.identity);
                _LastDropWeapon = dropWeapon.GetComponent<Collider>();
                dropWeapon.GetComponent<WeaponCollectActor>().WeaponScript = playerPerson.Weapon;
                dropWeapon.GetComponent<WeaponCollectActor>().itsPickUp = true;
                LastCollectWeapon = _static.allWeapons.Find(actor => actor.WeaponIndex == weaponActor.WeaponIndex);
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

    private void OnTriggerExit(Collider other)
    {
        if (other == _LastDropWeapon)
        {
            _LastDropWeapon.GetComponent<WeaponCollectActor>().itsPickUp = false;
        }
    }
}

internal struct PlayerData
{
    public Vector3 Direction;
}
