using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class EnemyMoveSystem : IEcsRunSystem
    {
        private EcsFilter<RushersData, EnemyData,PersonData>.Exclude<DieFlag> _rushers;
        private EcsFilter<PlayerData, PersonData>.Exclude<DieFlag> _persons;
        private EcsFilter<RushersData, Stop> _stopRushers;
        private EcsFilter<RushersData, Return> _ReturnRushers;
        private EcsFilter<ShootingSceletonData,PersonData>.Exclude<DieFlag> _sceletonShoot;
        private EcsFilter<ShootingSceletonData,Stop>.Exclude<DieFlag> _stopSceleton;
        private StaticData _static;
        private float _time;

        public void Run()
        {
            foreach (var item in _rushers)
            {
                ref var randomPersonActor = ref _persons.GetEntity(Random.Range(0, _persons.GetEntitiesCount() - 1)).Get<PersonData>().Actor;
                ref var rushers = ref _rushers.Get1(item);
                ref var animator = ref _rushers.Get3(item).Actor.Animator;

                rushers.meshAgent.SetDestination(randomPersonActor.transform.position);
                animator.SetBool("Run",true);
            }

            foreach (var item in _stopRushers)
            {
                ref var rushers = ref _stopRushers.Get1(item);
                var position = rushers.meshAgent.gameObject.transform.position;

                rushers.meshAgent.SetDestination(position);
            }
            
            foreach (var item in _stopSceleton)
            {
                ref var sceleton = ref _stopSceleton.Get1(item);
                var position = sceleton.meshAgent.gameObject.transform.position;

                sceleton.meshAgent.SetDestination(position);
            }

            foreach (var item in _ReturnRushers)
            {
                ref var entity = ref _ReturnRushers.GetEntity(item);

                if (entity.Has<Stop>()) entity.Del<Stop>();
                if (entity.Has<Return>()) entity.Del<Return>();
            }

            foreach (var item in _sceletonShoot)
            {
                ref var entity = ref _sceletonShoot.GetEntity(item);
                ref var sceleton = ref _sceletonShoot.Get1(item);
                ref var sceletonActor = ref _sceletonShoot.Get2(item).Actor;
                ref var sceletonAnimator = ref sceletonActor.Animator;
                ref var randomPersonActor = ref _persons.GetEntity(Random.Range(0, _persons.GetEntitiesCount() - 1)).Get<PersonData>().Actor;

                var distance = Vector3.Distance(sceletonActor.transform.position, randomPersonActor.transform.position);
                var distanceToPlayer = _static.sceletonDistanceToPlayer;
                var distanceToBack = _static.sceletonDistanceBackDistance;

                if (distance >= distanceToPlayer)
                {
                    if (entity.Has<Stop>()) entity.Del<Stop>();
                    sceletonAnimator.SetBool("Run", true);

                    sceleton.meshAgent.SetDestination(randomPersonActor.transform.position);
                }
                else if (distance < distanceToPlayer && distance > distanceToBack)
                {
                    entity.Get<Stop>();

                    if (_time < Time.time)
                    {
                        //Debug.Log("SceletonShoot");
                        entity.Get<ShootEvent>() = new ShootEvent()
                        {
                            
                            Weapon = sceletonActor.Weapon,
                            Transform = sceletonActor.Weapon.ShootPoint,
                            CountShoots = sceletonActor.Weapon.countShot
                        };

                        _time = Time.time + sceletonActor.Weapon.shotsTime;
                    }

                    sceletonAnimator.SetBool("Run", false);

                    var rotation = randomPersonActor.transform.position - sceletonActor.transform.position;
                    sceletonActor.transform.rotation = Quaternion.LookRotation(rotation);
                }
                else
                {
                    if (entity.Has<Stop>()) entity.Del<Stop>();
                    sceletonAnimator.SetBool("Run", true);

                    var position = sceletonActor.transform.position + (sceletonActor.transform.position - randomPersonActor.transform.position );
                    sceleton.meshAgent.SetDestination(position);
                }
            }
        }
    }
}