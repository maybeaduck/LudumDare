using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class EnemyActor : MonoBehaviour
{
    public PersonActor actor;
    void Start()
    {
        actor.ThisEntity.Get<EnemyData>();
    }

}

internal struct EnemyData
{
}
