using Leopotam.Ecs;

namespace Zlodey
{
    public class DamageSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<CharacterStatsComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
            }
        }
    }
}