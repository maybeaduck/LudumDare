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

                GameObject.Instantiate(shootFX, transform);
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
                        GameObject.Instantiate(hitEnemyFX, transform.position, transform.rotation);
                        break;
                    case "Wall":
                        GameObject.Instantiate(hitWallFX, transform.position, transform.rotation);
                        break;
                }

                entity.Del<HitBulletEvent>();
            }
        }
    }
}