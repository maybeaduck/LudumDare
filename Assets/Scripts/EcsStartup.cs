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
        private EcsFilter<PersonData, DieFlag> _owerDie;
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
        }
    }

    internal struct DieFlag
    {
    }
}