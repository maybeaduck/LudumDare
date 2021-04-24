using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class EnemyActor : MonoBehaviour
{
    public PersonActor actor;
    IEnumerator Start()
    {
        yield return null;
        actor.ThisEntity.Get<EnemyData>();
        
    }

}

internal struct EnemyData
{
}
