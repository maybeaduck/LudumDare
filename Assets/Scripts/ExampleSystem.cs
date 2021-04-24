using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class ExampleSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<WinEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            { 
            }
        }
    }
    
    public class ShootSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PointComponent, ShootEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var transform = ref _filter.Get1(item).Transform;

                var bulletPrefab = Config.Bullet;
                var speed = Config.BulletSpeed;
                var bullet = GameObject.Instantiate(bulletPrefab, transform);

                bullet.Rigidbody.velocity = bullet.transform.forward * speed;
            }
        }
    }

    internal struct PointComponent
    {
        public Vector3 Position;
        public Transform Transform;
    }

    internal struct ShootEvent
    {
    }
}