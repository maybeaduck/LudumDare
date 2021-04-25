using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public PersonActor playerPerson;

    private IEnumerator Start()
    {
        yield return null;
        playerPerson.ThisEntity.Get<PlayerData>();
        
    }
}

internal struct PlayerData
{
    public float Speed;
}
