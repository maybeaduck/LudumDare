using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class ShootFXSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<ShootEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var transform = ref _filter.Get1(item).Transform;
                var fxPrefab = Config.ShootFX;

                GameObject.Instantiate(fxPrefab, transform);
            }
        }
    }
}