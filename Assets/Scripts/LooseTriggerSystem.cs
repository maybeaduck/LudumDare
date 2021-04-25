using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class LooseTriggerSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<PlayerData>.Exclude<DieFlag> _livePlayers;
        private EcsFilter<PlayerData, DieFlag> _diePlayers;
        private EcsFilter<LoseEvent> _lose;
        public void Run()
        {
            if (_livePlayers.IsEmpty() && !_diePlayers.IsEmpty()  && _lose.IsEmpty())
            {
                Debug.Log("YouLose");
                _world.NewEntity().Get<LoseEvent>();
            }
        }
    }
}