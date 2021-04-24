using System;using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
public enum EnemyType{
    Rushers
}
public class EnemyActor : MonoBehaviour
{
    public PersonActor actor;
    public EnemyType EnemyType;
    IEnumerator Start()
    {
        yield return null;
        actor.ThisEntity.Get<EnemyData>();
        
    }

}

internal struct EnemyData
{
}
