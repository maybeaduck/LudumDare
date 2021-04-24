using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class PersonControlSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PersonData,PlayerData>.Exclude<StandFlag> _activePersons;
        public void Run()
        {

            foreach (var item in _activePersons)
            {
                ref var person = ref _activePersons.Get1(item).Actor;
                ref var rigidbody = ref _activePersons.Get1(item).Rigidbody;
                var speed = Config.speed;
                var normalDirection = new Vector3(
                    Input.GetAxis("Horizontal") * speed,
                    person.transform.position.y,
                    Input.GetAxis("Vertical") * speed);

                rigidbody.velocity = -normalDirection;

                
                var isRun = normalDirection.magnitude > 1 ? true : false;
                person.Animator.SetBool("Run", isRun);


            }
        }
    }
}