using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class DashSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PersonData> _filter;
        private float _time;

        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var animator = ref _filter.Get1(item).Actor.Animator;

                if (_time < Time.time)
                {
                    if (entity.Has<Dash>()) entity.Del<Dash>();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (_time < Time.time)
                    {
                        animator.SetTrigger("Dash");
                        entity.Get<Dash>();

                        var cooldownTime = Config.DashCooldownTime;
                        _time = Time.time + cooldownTime;

                        return;
                    }
                }
            }
        }
    }
}