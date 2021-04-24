using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class Bullet : MonoBehaviour,IPooledObject
    {
        public Rigidbody Rigidbody;
        public Weapon Weapon;
        public void OnObjectSpawn()
        {
            
        }

        public void SetPool(Queue<GameObject> pool)
        {
            Debug.Log("ИДИ ОТСЮДА БАГ УХАДИ");
        }
    }
}