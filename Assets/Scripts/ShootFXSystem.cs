using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class ShootFXSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<ShootEvent> _shootFilter;
        private EcsFilter<HitBulletEvent> _hitFilter;
        private EcsFilter<BoomEvent,BoomFlag,BulletData> _boomFilter;
        public void Run()
        {
            foreach (var item in _shootFilter)
            {
                ref var transform = ref _shootFilter.Get1(item).Transform;
                var a = ObjectPoolController.Instance.SpawnFromPool("shootFX", transform.position, transform.rotation).GetComponent<PoolFX>();
                a.gameObject.SetActive(true);
                a.particleSystem.Play();
            }

            foreach (var item in _hitFilter)
            {
                ref var entity = ref _hitFilter.GetEntity(item);
                ref var target = ref _hitFilter.Get1(item).Target;
                ref var transform = ref _hitFilter.Get1(item).OnHitTransform;
                ref var damage = ref _hitFilter.Get1(item).DamageValue;

                switch (target.tag)
                {
                    case "Enemy":
                        var fx =ObjectPoolController.Instance.SpawnFromPool("hitEnemyFX", transform.position, transform.rotation).GetComponent<PoolFX>();
                        var damageFX = ObjectPoolController.Instance.SpawnFromPool("damageFX", transform.position, transform.rotation).GetComponent<PoolFX>();
                        damageFX.DamageFX.SetValue(damage);
                        fx.particleSystem.Play();
                        break;
                    case "Player":
                        ObjectPoolController.Instance.SpawnFromPool("hitPlayerFX", transform.position, transform.rotation).GetComponent<PoolFX>();
                        break;
                    case "Wall":
                        var fx2 =ObjectPoolController.Instance.SpawnFromPool("hitWallFX", transform.position, transform.rotation).GetComponent<PoolFX>();
                        fx2.particleSystem.Play();
                        break;
                }

                entity.Del<HitBulletEvent>();
            }

            foreach (var item in _boomFilter)
            {
                ref var entity = ref _boomFilter.GetEntity(item);
                ref var position = ref _boomFilter.Get1(item).Position;
                ref var boom = ref _boomFilter.Get2(item);
                ref var bullet = ref _boomFilter.Get3(item);
                var BoomActor =ObjectPoolController.Instance.SpawnFromPool("boomFX", position, Quaternion.identity).GetComponent<PoolFX>();
                
                if (!entity.Has<BoomParticle>())
                {
                    Debug.Log("Boom!");
                    BoomActor.particleSystem.Play();
                    entity.Get<BoomParticle>();
                }
                
                
                if (boom.BoomActor.BoomColider.radius < boom.BoomSize)
                {
                    Debug.Log("BoomEvent1");
                    boom.BoomActor.BoomColider.radius += boom.BoomSize * Time.deltaTime * boom.BoomSpeed;
                }
                else
                {
                    Debug.Log("BoomEvent2");
                    bullet.Bullet.gameObject.SetActive(false);
                    _boomFilter.GetEntity(item).Destroy();
                }
                
            }
        }
    }

    public struct BoomParticle
    {
    }
}