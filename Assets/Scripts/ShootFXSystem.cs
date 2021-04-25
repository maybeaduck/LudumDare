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
                var shootFX = Config.ShootFX;

                ObjectPoolController.Instance.SpawnFromPool("shootFX", transform.position, transform.rotation).GetComponent<PoolFX>();
            }

            foreach (var item in _hitFilter)
            {
                ref var entity = ref _hitFilter.GetEntity(item);
                ref var target = ref _hitFilter.Get1(item).Target;
                ref var transform = ref _hitFilter.Get1(item).OnHitTransform;

                var hitEnemyFX = Config.HitEnemyFX;
                var hitWallFX = Config.HitWallFX;

                switch (target.tag)
                {
                    case "Enemy":
                        ObjectPoolController.Instance.SpawnFromPool("hitEnemyFX", transform.position, transform.rotation).GetComponent<PoolFX>();
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