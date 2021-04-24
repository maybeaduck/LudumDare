using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class TestDamageSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private StaticData _static;
        private EcsFilter<CharacterStatsComponent, PlayerData> _player;
        public void Run()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                foreach (var item in _player)
                {
                    _player.GetEntity(item).Get<DamageEvent>().Value = _static.TestDamage;
                }
                
            }
            if (Input.GetKeyUp(KeyCode.H))
            {
                foreach (var item in _player)
                {
                    _player.GetEntity(item).Get<DamageEvent>().Value = -10;
                }
                
            }
        }
    }
}