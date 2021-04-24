using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

public class WallActor : MonoBehaviour
{
    private EcsWorld _world;

    private void Start()
    {
        _world = Service<EcsWorld>.Get();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            var View = other.GetComponent<ViewBullet>();
            _world.NewEntity().Get<HitBulletEvent>() = new HitBulletEvent()
            {
                OnHitTransform = View.actor.transform,Target = this.gameObject
            };
            View.actor.gameObject.SetActive(false);
        }
    }
}
