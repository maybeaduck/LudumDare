using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

public class PersonActor : MonoBehaviour
{
    public Animator Animator;
    public EcsEntity ThisEntity;
    public Rigidbody Rigidbody;

    void Start()
    {
        var world = Service<EcsWorld>.Get();
        ThisEntity = world.NewEntity();
        ThisEntity.Get<PersonData>() = new PersonData()
        { 
            Rigidbody = Rigidbody,
            Actor = this
        };
    }

    
}

internal struct PersonData
{
    public PersonActor Actor;
    public Rigidbody Rigidbody;
}
