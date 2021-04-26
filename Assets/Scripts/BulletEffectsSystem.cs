using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class BulletEffectsSystem : Injects,IEcsRunSystem
    {
        private EcsFilter<BulletData, BoomFlag> _boom;
        private EcsFilter<BulletData, DestroyFlag> _destroy;
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

            foreach (var item in _destroy)
            {
                ref var bullet = ref _destroy.Get1(item);
                ref var boom = ref _destroy.Get2(item);

                bullet.LiveTime += Time.deltaTime;
                if (bullet.LiveTime >= bullet.MaxLiveTime  )
                {
                    bullet.Bullet.gameObject.SetActive(false);
                    _destroy.GetEntity(item).Destroy();
                }
            }
        }
    }
}