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
                ref var weapon = ref _filter.Get1(item).Weapon;
                var bulletPrefab = Config.Bullet;
                var speed = Config.BulletSpeed;
                
                var bullet = ObjectPoolController.Instance.SpawnFromPool("bullet",transform.position,transform.rotation).GetComponent<Bullet>();
                bullet.Weapon = weapon;

                bullet.transform.localEulerAngles =bullet.transform.localEulerAngles + new Vector3(Random.Range(-bullet.Weapon.spread, bullet.Weapon.spread),
                    0, Random.Range(-bullet.Weapon.spread, bullet.Weapon.spread));
                
                var direction = (bullet.transform.forward  ) * speed;
                
                
                bullet.Rigidbody.velocity = direction ; // * speed;
            }
        }
    }
}