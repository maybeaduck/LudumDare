using System;using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;
using Zlodey;

public enum EnemyType{
    Rushers
}
public class EnemyActor : MonoBehaviour
{
    public PersonActor actor;
    public EnemyType EnemyType;
    public NavMeshAgent agent;
    public float botSpeed;
    public Collider enemyBarrier;
        IEnumerator Start()
    {
        yield return null;
        actor.ThisEntity.Get<EnemyData>();
        switch (EnemyType)
        {
            case EnemyType.Rushers :
                actor.ThisEntity.Get<RushersData>() = new RushersData(){meshAgent = agent,botSpeed = botSpeed};
                break;
        }
    }

}

internal struct EnemyData
{
}
