using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;
using Zlodey;

public class PersonActor : MonoBehaviour
{
    public Animator Animator;
    public EcsEntity ThisEntity;
    public Rigidbody Rigidbody;
    public CharacterStats StatsComponent;
    public Weapon Weapon;

    void Start()
    {
        var world = Service<EcsWorld>.Get();
        ThisEntity = world.NewEntity();
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
            var View = other.GetComponent<ViewBullet>();
            
            ThisEntity.Get<DamageEvent>() = new DamageEvent() { Value = View.actor.Weapon.Damage};
        }
    }
}

internal struct PersonData
{
    public PersonActor Actor;
    public Rigidbody Rigidbody;
    public Weapon Weapon;
}
