using Leopotam.Ecs;

namespace Zlodey
{
    public class HealthSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<CharacterStatsComponent, DamageEvent>.Exclude<DieEvent,DieFlag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var characterStats = ref _filter.Get1(i).CharacterStats;
                ref var damage = ref _filter.Get2(i).Value;

                characterStats.Health.Value -= damage;

                if (characterStats.Health.Value <= 0)
                {
                    characterStats.Health.Value = 0;
                    _filter.GetEntity(i).Get<DieEvent>();
                }
                
                
                if (characterStats.Health.Value > _filter.Get1(i).FullHealth)
                {
                    characterStats.Health.Value = _filter.Get1(i).FullHealth;
                }
                characterStats.Health.HealthSlider.value = characterStats.Health.Value;
                _filter.GetEntity(i).Del<DamageEvent>();
            }
        }
    }

    public struct DieEvent
    {
    }
}