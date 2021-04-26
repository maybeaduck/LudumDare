using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class PersonControlSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PersonData,PlayerData>.Exclude<StandFlag,DieFlag> _activePersons;
        public void Run()
        {
            foreach (var item in _activePersons)
            {
                ref var entity = ref _activePersons.GetEntity(item);
                if (entity.Has<Dash>())
                {
                    return;
                }

                ref var person = ref _activePersons.Get1(item).Actor;
                ref var rigidbody = ref _activePersons.Get1(item).Rigidbody;
                ref var direction = ref _activePersons.Get2(item).Direction;

                var speed = _staticData.speed;

                direction = new Vector3(
                    Input.GetAxis("Horizontal"),
                    0,
                    Input.GetAxis("Vertical")).normalized * speed;
                
                rigidbody.velocity = -direction;

                var isRun = direction.magnitude > 1 ? true : false;
                person.Animator.SetBool("Run", isRun);
            }
        }
    }
}