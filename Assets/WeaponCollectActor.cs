using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;
using Zlodey;

public class WeaponCollectActor : MonoBehaviour
{
    public EcsEntity ThisEntity;
    public GameObject WeaponModel;
    public int WeaponIndex;
    public Animator Animator;
    public Weapon WeaponScript;
    public bool itsPickUp;

    public void Start()
    {
        var world = Service<EcsWorld>.Get();
        ThisEntity = world.NewEntity();
        ThisEntity.Get<DropData>() = new DropData(){collectActor = this,weapon = WeaponScript,time = 0};

    }
}

public struct DropData
{
    public WeaponCollectActor collectActor;
    public Weapon weapon;
    public float time;
}

