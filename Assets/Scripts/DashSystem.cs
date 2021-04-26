using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    public class DashSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PersonData,PlayerData>.Exclude<StandFlag, DieFlag> _filter;
        private EcsFilter<PersonData, PlayerData, Dash>.Exclude<StandFlag, DieFlag> _dashPersons;

        private float _time;

        public void Run()
        {
            if (_runtimeData.GameState != GameState.Play)
            {
                return;
            }
            foreach (var item in _filter)
            {
                ref var entity = ref _filter.GetEntity(item);
                ref var animator = ref _filter.Get1(item).Actor.Animator;
                ref var rigidbody = ref _filter.Get1(item).Actor.Rigidbody;

                ref var direction = ref _filter.Get2(item).Direction;
                var dashSpeed = _staticData.DashSpeed;

                if (_time < Time.time)
                {
                    if (entity.Has<Dash>())
                    {
                        entity.Del<Dash>();
                    }
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (_time < Time.time)
                    {
                        if (direction.magnitude < 1) return;

                        rigidbody.velocity = -direction.normalized * dashSpeed;
                        animator.SetTrigger("Dash");
                        entity.Get<Dash>();

                        var cooldownTime = _staticData.DashCooldownTime;
                        _time = Time.time + cooldownTime;

                        _runtimeData.AudioSource.PlayOneShot(_staticData.DashAudio,1); //audio

                        return;
                    }
                }
            }

            foreach (var item in _dashPersons)
            {
                ref var person = ref _dashPersons.Get1(item).Actor;
                ref var direction = ref _dashPersons.Get2(item).Direction;

                person.transform.rotation = Quaternion.LookRotation(-direction);
            }
        }
    }
}