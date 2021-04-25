using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class DieSystem : IEcsRunSystem
    {
        private EcsFilter<PersonData, CharacterStatsComponent, DieEvent> _die;
        private EcsFilter<ShootingSceletonData, DieFlag> _dieShotingSceleton;
        private EcsFilter<PersonData, DieFlag> _owerDie;
        private EcsFilter<RushersData, DieFlag> _rushersDie;
        public void Run()
        {
            foreach (var item in _die)
            {
                ref var personData = ref _die.Get1(item);
                personData.Actor.Animator.SetBool("Die",true);
                personData.Actor.GetComponent<Collider>().enabled = false;
                personData.Rigidbody.isKinematic = true;
                personData.Actor.HealthBar.SetActive(false);
                _die.GetEntity(item).Get<DieFlag>();
                _die.GetEntity(item).Del<DieEvent>();
                
            }

            foreach (var item in _rushersDie)
            {
                _rushersDie.Get1(item).botFilter.enabled = false;
                _rushersDie.Get1(item).AttackZone.enabled = false;
                _rushersDie.Get1(item).meshAgent.Stop(true);
            }

            foreach (var item in _dieShotingSceleton)
            {
                _dieShotingSceleton.Get1(item).meshAgent.Stop(true);
            }
        }
    }
}