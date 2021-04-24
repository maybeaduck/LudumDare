using Leopotam.Ecs;

namespace Zlodey
{
    public class HealthSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<CharacterStatsComponent, DamageEvent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var characterStats = ref _filter.Get1(i).CharacterStats;
                ref var damage = ref _filter.Get2(i).Value;

                characterStats.Health.Value -= damage;

                if (characterStats.Health.Value < 0)
                {
                    characterStats.Health.Value = 0;
                }
            }
        }
    }
}