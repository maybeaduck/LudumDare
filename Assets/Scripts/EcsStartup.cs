using System.Collections;
using System.Collections.Generic;
using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Zlodey
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;

        public StaticData _config;
        public SceneData _scene;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            var _runtime = new RuntimeData();
            Service<RuntimeData>.Set(_runtime);

            UI _ui = GetOrSetUI(_config);

            Service<EcsWorld>.Set(_world);
            Service<StaticData>.Set(_config);
            Service<SceneData>.Set(_scene);

            _systems
                .Add(new InitializeSystem())
                .Add(new ChangeGameStateSystem())
                .Add(new EnemyMoveSystem())
                .Add(new PersonControlSystem())
                .Add(new PersonLookAtMouseSystem())
                .Add(new HealthSystem())
                .Add(new TestDamageSystem())
                
                .Add(new DieSystem())
                .Add(new LooseTriggerSystem())
                .Add(new WinSystem())
                .Add(new LoseSystem())
                .Add(new ShootingSystem())
                .Add(new MoveBulletSystem())
                .Add(new BulletEffectsSystem())
                .Add(new ShootFXSystem())
                .Add(new DashSystem())

                .OneFrame<ShootEvent>()

                .Inject(_runtime)
                .Inject(_config)
                .Inject(_scene)
                .Inject(_ui)
                .Init();
            
        }

        public static UI GetOrSetUI(StaticData staticData)
        {
            var ui = Service<UI>.Get();
            if (!ui)
            {
                ui = Instantiate(staticData.UIPrefab);
                Service<UI>.Set(ui);
            }

            return ui;
        }

        void Update() => _systems?.Run();

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }

    internal class BulletEffectsSystem : Injects,IEcsRunSystem
    {
        private EcsFilter<BulletData, BoomFlag> _boom;
        public void Run()
        {
            foreach (var item in _boom)
            {
                ref var bullet = ref _boom.Get1(item);
                ref var boom = ref _boom.Get2(item);

                bullet.LiveTime += Time.deltaTime;
                if (bullet.LiveTime >= bullet.MaxLiveTime)
                {
                    Debug.Log("BoomEvent");
                    _boom.GetEntity(item).Get<BoomEvent>().Position = bullet.Bullet.transform.position;
                    if (boom.BoomActor.BoomColider.radius < boom.BoomSize)
                    {
                        Debug.Log("BoomEvent1");
                        boom.BoomActor.BoomColider.radius += boom.BoomSize * Time.deltaTime * boom.BoomSpeed;
                    }
                    else
                    {
                        Debug.Log("BoomEvent2");
                        bullet.Bullet.gameObject.SetActive(false);
                        _boom.GetEntity(item).Destroy();
                    }
                }

            }
        }
    }

    internal struct BoomEvent
    {
        public Vector3 Position;
    }


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
                        entity.Get<ShootEvent>() = new ShootEvent()
                        {
                            Weapon = sceletonActor.Weapon,
                            Transform = sceletonActor.Weapon.ShootPoint
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

    internal struct Return
    {
    }

    internal struct Stop
    {
    }

    internal struct RushersData
    {
        public NavMeshAgent meshAgent;
        public float botSpeed;
        public Collider botFilter;
        public Collider AttackZone;
    }

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

    internal struct DieFlag
    {
    }
}