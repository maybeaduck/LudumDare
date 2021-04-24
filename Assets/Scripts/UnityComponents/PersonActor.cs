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
    void Start()
    {
        var world = Service<EcsWorld>.Get();
        ThisEntity = world.NewEntity();
        ThisEntity.Get<PersonData>() = new PersonData()
        { 
            Rigidbody = Rigidbody,
            Actor = this
        };
        ThisEntity.Get<CharacterStatsComponent>() = new CharacterStatsComponent(){CharacterStats = StatsComponent,FullHealth = StatsComponent.Health.Value};
    }

    
}

internal struct PersonData
{
    public PersonActor Actor;
    public Rigidbody Rigidbody;
}
