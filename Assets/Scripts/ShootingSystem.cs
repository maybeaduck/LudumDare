using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class ShootingSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PersonData> _filter;

        private float _time;
        public void Run()
        {
            foreach (var item in _filter)
            {
                if (Input.GetMouseButton(0))
                {
                    if (_time < Time.time)
                    {
                        ref var entity = ref _filter.GetEntity(item);
                        ref var shootPoint = ref _filter.Get1(item).Weapon.ShootPoint;
                        entity.Get<ShootEvent>().Transform = shootPoint;

                        var cooldownTime = Config.BulletCooldownTime;
                        _time = Time.time + cooldownTime;
                        return;
                    }
                }
            }
        }
    }
}