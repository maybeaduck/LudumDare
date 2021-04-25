using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class BulletEffectsSystem : Injects,IEcsRunSystem
    {
        private EcsFilter<BulletData, BoomFlag> _boom;
        public void Run()
        {
            foreach (var item in _boom)
            {
                ref var bullet = ref _boom.Get1(item);
                ref var boom = ref _boom.Get2(item);

                bullet.LiveTime += Time.deltaTime;
                if (bullet.LiveTime >= bullet.MaxLiveTime  )
                {
                    
                    
                    Debug.Log("BoomEvent");     
                    _boom.GetEntity(item).Get<BoomEvent>().Position = bullet.Bullet.transform.position;    
                    
                    
                }

            }
        }
    }
}