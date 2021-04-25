using Leopotam.Ecs;

namespace Zlodey
{
    internal class PersonUpdateSpeedSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PlayerData> _filter;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var speed = ref _filter.Get1(item).Speed;

                speed = !entity.Has<Dash>() ? Config.speed : Config.DashSpeed;
            }
        }
    }
}