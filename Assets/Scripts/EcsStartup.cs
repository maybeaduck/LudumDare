using System.Collections;
using System.Collections.Generic;
using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

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
                .Add(new PersonControlSystem())
                .Add(new HealthSystem())
                .Add(new TestDamageSystem())
                .Add(new PersonLookAtMouseSystem())
                .Add(new WinSystem())
                .Add(new LoseSystem())
                .Add(new ShootSystem())
                

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

                if (Mathf.Abs(normalDirection.x) > 0 || Mathf.Abs(normalDirection.z) > 0)
                {
                    person.Animator.SetBool("Run", true);
                }
                else
                {
                    person.Animator.SetBool("Run", false);
                }


            }
        }
    }

    internal class PersonLookAtMouseSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private RuntimeData _runtime;
        private SceneData _scene;
        private StaticData _static;
        private EcsFilter<PersonData,PlayerData>.Exclude<StandFlag> _activePersons;
        public void Run()
        {
            
            foreach (var item in _activePersons)
            {
                Debug.Log("WhoNotWork");
                ref var person = ref _activePersons.Get1(item).Actor;
                var position = person.transform.position;
                Plane plane = new Plane(Vector3.up,position);
                Ray ray = _scene.Camera.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(ray, out var hit))
                {
                    Vector3 target = ray.GetPoint(hit);
                    Quaternion rotation = Quaternion.LookRotation(target - position);
                    person.transform.rotation = Quaternion.Slerp(person.transform.rotation, rotation,
                        _static.speedRotation * Time.deltaTime);

                }
            }
        }
    }

    internal struct StandFlag
    {
    }
}