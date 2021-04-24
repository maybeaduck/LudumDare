using Leopotam.Ecs;

namespace Zlodey
{
    public class DamageSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<CharacterStatsComponent,DamageEvent> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                _filter.Get1(item).CharacterStats.Health.Value -= _filter.Get2(item).Value;
                _filter.GetEntity(item).Destroy();
            }
        }
    }
}