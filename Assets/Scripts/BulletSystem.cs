using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class MoveBulletSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<ShootEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var transform = ref _filter.Get1(item).Transform;

                var bulletPrefab = Config.Bullet;
                var speed = Config.BulletSpeed;
                var bullet = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
                var direction = bullet.transform.forward * speed;

                bullet.Rigidbody.velocity = direction;
            }
        }
    }
}