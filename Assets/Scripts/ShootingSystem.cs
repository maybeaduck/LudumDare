using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class ShootingSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PersonData, PlayerData>.Exclude<Reload,DieFlag> _filter;
        private EcsFilter<PersonData, Reload> _reload;
        private float _time;
        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var weapon = ref _filter.Get1(item).Weapon;
                ref var entityPlayer = ref _filter.GetEntity(item);
                if (entityPlayer.Has<Dash>()) return;

                if (Input.GetKeyDown(KeyCode.R))
                {
                    entityPlayer.Get<Reload>();
                }
                if (Input.GetMouseButton(0))
                {
                    
                    if (_time < Time.time && weapon.ammunition > 0)
                    {
                        weapon.ammunition--;
                        ref var entity = ref entityPlayer;
                        ref var shootPoint = ref weapon.ShootPoint;
                        entity.Get<ShootEvent>() = new ShootEvent(){Transform = shootPoint,Weapon = weapon,CountShoots = weapon.countShot};

                        var cooldownTime = Config.BulletCooldownTime;
                        _time = Time.time + cooldownTime;
                        return;
                    }

                    if (weapon.ammunition == 0 && !entityPlayer.Has<Reload>())
                    {
                        entityPlayer.Get<Reload>();
                    }
                }
            }

            foreach (var item in _reload)
            {
                ref var playerData = ref _reload.Get1(item);
                ref var time = ref _reload.Get2(item).timeReload;
                time += Time.deltaTime;
                ref var allAmunition =ref playerData.Weapon.AllAmunitionInInvent;
                if (time >= playerData.Weapon.reloadTime&& allAmunition > 0)
                {
                    //allamunition - 35 if(>0){}else{allamunition= amuton ; allamunition = 0}
                    ref var defaultAmunition = ref playerData.Weapon.defaultAmunition;
                    if (allAmunition - defaultAmunition >= defaultAmunition)
                    {
                        playerData.Weapon.ammunition = defaultAmunition;
                        allAmunition -= defaultAmunition;
                    }
                    else
                    {
                        playerData.Weapon.ammunition = allAmunition;
                        allAmunition = 0;
                    }
                    _reload.GetEntity(item).Del<Reload>();
                }
            }
        }
    }

    public struct Reload
    {
        public float timeReload;
    }
}