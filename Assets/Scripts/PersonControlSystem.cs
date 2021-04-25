using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class PersonControlSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PersonData,PlayerData>.Exclude<StandFlag,DieFlag> _activePersons;
        private EcsFilter<PersonData,PlayerData, Dash>.Exclude<StandFlag,DieFlag> _dashPersons;
        public void Run()
        {
            foreach (var item in _activePersons)
            {
                ref var entity = ref _activePersons.GetEntity(item);
                if (entity.Has<Dash>()) return;

                ref var person = ref _activePersons.Get1(item).Actor;
                ref var rigidbody = ref _activePersons.Get1(item).Rigidbody;
                ref var speed = ref _activePersons.Get2(item).Speed;
                ref var direction = ref _activePersons.Get2(item).Direction;

                direction = new Vector3(
                    Input.GetAxis("Horizontal"),
                    0,
                    Input.GetAxis("Vertical")).normalized * speed;
                
                rigidbody.velocity = -direction;

                var isRun = direction.magnitude > 1 ? true : false;
                person.Animator.SetBool("Run", isRun);
            }

            foreach (var item in _dashPersons)
            {
                ref var rigidbody = ref _activePersons.Get1(item).Rigidbody;
                ref var direction = ref _dashPersons.Get2(item).Direction;
                ref var speed = ref _activePersons.Get2(item).Speed;

                rigidbody.velocity = -direction.normalized * speed;
            }
        }
    }
}