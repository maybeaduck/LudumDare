using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;
using Zlodey;
using Random = UnityEngine.Random;

public class PersonActor : MonoBehaviour
{
    public Animator Animator;
    public EcsEntity ThisEntity;
    public Rigidbody Rigidbody;
    public CharacterStats StatsComponent;
    public Weapon Weapon;

    private EcsWorld _world;
    public GameObject HealthBar;

    void Start()
    {
        _world = Service<EcsWorld>.Get();
        ThisEntity = _world.NewEntity();
        ThisEntity.Get<PersonData>() = new PersonData()
        { 
            Weapon = Weapon,
            Rigidbody = Rigidbody,
            Actor = this
        };
        ThisEntity.Get<CharacterStatsComponent>() = new CharacterStatsComponent(){CharacterStats = StatsComponent,FullHealth = StatsComponent.Health.Value};
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("BulletsPOPALO " + gameObject.name);
            var View = other.GetComponent<ViewBullet>();
            
            ThisEntity.Get<DamageEvent>().Value += Random.Range(View.actor.Weapon.MinDamage,View.actor.Weapon.MaxDamage);
            _world.NewEntity().Get<HitBulletEvent>() = new HitBulletEvent()
            {
                OnHitTransform = View.actor.transform,Target = this.gameObject,
                DamageValue = Random.Range(View.actor.Weapon.MinDamage,View.actor.Weapon.MaxDamage)
            };
            if (!View.actor.SniperBullet)
            {
                Debug.Log(" Я ТУПАЯ ПУЛЯ");
                View.actor.gameObject.SetActive(false);    
            }
            
        }
    }
}

internal struct HitBulletEvent
{
    public Transform OnHitTransform;
    public GameObject Target;
    public float DamageValue;
}

internal struct PersonData
{
    public PersonActor Actor;
    public Rigidbody Rigidbody;
    public Weapon Weapon;
}
