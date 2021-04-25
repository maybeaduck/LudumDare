using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class ShootFXSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<ShootEvent> _shootFilter;
        private EcsFilter<HitBulletEvent> _hitFilter;
        public void Run()
        {
            foreach (var item in _shootFilter)
            {
                ref var transform = ref _shootFilter.Get1(item).Transform;
                ObjectPoolController.Instance.SpawnFromPool("shootFX", transform.position, transform.rotation).GetComponent<PoolFX>();
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
                        ObjectPoolController.Instance.SpawnFromPool("hitEnemyFX", transform.position, transform.rotation).GetComponent<PoolFX>();
                        var damageFX = ObjectPoolController.Instance.SpawnFromPool("damageFX", transform.position, transform.rotation).GetComponent<PoolFX>();
                        damageFX.GetComponent<DamageFX>().SetValue(damage);
                        break;
                    case "Wall":
                        ObjectPoolController.Instance.SpawnFromPool("hitWallFX", transform.position, transform.rotation).GetComponent<PoolFX>();
                        break;
                }

                entity.Del<HitBulletEvent>();
            }
        }
    }
}