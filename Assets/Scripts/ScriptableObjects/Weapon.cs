using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Zlodey
{
    public class Weapon : MonoBehaviour
    {
        public Transform ShootPoint;
        private float _time;

        private void Update()
        {
            var cooldownTime = Service<StaticData>.Get().BulletCooldownTime;

            if (_time < Time.time)
            {

                _time = Time.time + cooldownTime;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                var entity = Service<EcsWorld>.Get().NewEntity();
                entity.Get<PointComponent>().Transform = ShootPoint;
            }
        }
    }
}