using Leopotam.Ecs;
using LeopotamGroup.Globals;
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
                var speed = weapon.bulletSpeed;
                for (int i = 0; i < _filter.Get1(item).CountShoots; i++)
                {
                    var bullet = ObjectPoolController.Instance.SpawnFromPool(weapon.BulletType,transform.position,transform.rotation).GetComponent<Bullet>();
                    bullet.Weapon = weapon;
                    bullet.entity = World.NewEntity();
                    bullet.entity.Get<BulletData>() = new BulletData(){MaxLiveTime = weapon.MaxLiveTime,Bullet = bullet};
                    switch (weapon.BulletType)
                    {
                        case "bulletBoom":
                            bullet.entity.Get<BoomFlag>() = new BoomFlag(){BoomSize = weapon.BoomSize,BoomSpeed = weapon.BoomSpeed,BoomActor = bullet.Boom};
                            break;
                        default:
                            
                            break;
                    }
                    var direction = (bullet.transform.forward  ) * speed;
                    direction =direction + new Vector3(Random.Range(-bullet.Weapon.spread, bullet.Weapon.spread),
                        0, Random.Range(-bullet.Weapon.spread, bullet.Weapon.spread));
                
                    bullet.Rigidbody.velocity = direction ; 
                }
                
            }
        }
    }

    public struct BoomFlag
    {
        public float BoomSize;
        public float BoomSpeed;
        public BoomActor BoomActor;
    }

    public struct BulletData
    {
        public Bullet Bullet;
        public float LiveTime;
        public float MaxLiveTime;
    }
}