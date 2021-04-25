using System.Collections;
using System.Collections.Generic;
using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
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

    internal class EnemyMoveSystem : IEcsRunSystem
    {
        private EcsFilter<RushersData, EnemyData,PersonData>.Exclude<DieFlag> _rushers;
        private EcsFilter<PlayerData, PersonData>.Exclude<DieFlag> _persons;
        private EcsFilter<RushersData, Stop> _stopRushers;
        private EcsFilter<RushersData, Return> _ReturnRushers;
        private EcsFilter<ShootingSceletonData,PersonData>.Exclude<DieFlag> _sceletonShoot;
        private StaticData _static;

        public void Run()
        {
            foreach (var item in _rushers)
            {
                ref var rushers = ref _rushers.Get1(item);
                rushers.meshAgent.SetDestination(_persons.GetEntity(Random.Range(0, _persons.GetEntitiesCount() - 1))
                    .Get<PersonData>().Actor.transform.position);
                _rushers.Get3(item).Actor.Animator.SetBool("Run",true);
            }

            foreach (var item in _stopRushers)
            {
                _stopRushers.Get1(item).meshAgent.Stop(true);
                
            }

            foreach (var item in _ReturnRushers)
            {
                // _ReturnRushers.Get1(item).meshAgent.isStopped = false;
                _ReturnRushers.GetEntity(item).Del<Stop>();
                _ReturnRushers.GetEntity(item).Del<Return>();
            }

            foreach (var item in _sceletonShoot)
            {
                ref var sceletons = ref _sceletonShoot.Get1(item);
                if (Vector3.Distance(_sceletonShoot.Get2(item).Actor.transform.position,
                    _persons.GetEntity(Random.Range(0, _persons.GetEntitiesCount() - 1)).Get<PersonData>().Actor
                        .transform.position) > _static.sceletonDistanceToPlayer)
                {
                    sceletons.meshAgent.SetDestination(_persons
                        .GetEntity(Random.Range(0, _persons.GetEntitiesCount() - 1))
                        .Get<PersonData>().Actor.transform.position);
                    _rushers.Get3(item).Actor.Animator.SetBool("Run", true);
                }
                else
                {
                    sceletons.meshAgent.SetDestination(_sceletonShoot.Get2(item).Actor.transform.position -
                                                       Vector3.back * _static.sceletonDistanceBackDistance);
                    _rushers.Get3(item).Actor.Animator.SetBool("Run", false);
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