using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class Bullet : MonoBehaviour,IPooledObject
    {
        public Rigidbody Rigidbody;
        public Weapon Weapon;
        public bool SniperBullet;
        public EcsEntity entity;
        public BoomActor Boom;
        public float startSizeCollider;
        public SphereCollider collider;
        public void OnObjectSpawn()
        {
            
        }

        public void SetPool(Queue<GameObject> pool)
        {
            //Debug.Log("ИДИ ОТСЮДА БАГ УХАДИ");
            collider.radius = startSizeCollider;
            
        }
    }

    
}