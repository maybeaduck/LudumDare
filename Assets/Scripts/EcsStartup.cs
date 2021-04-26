using System.Collections;
using System.Collections.Generic;
using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEditor;
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
                .Add(new RemoveSystem())
                .Add(new CheckDeadEnemiesSystem())
                .Add(new WaveSystem())
                .Add(new SpawnSystem())
                .Add(new NextFloorSystem())
                .Add(new WeaponUIUpdateSystem())

                .OneFrame<ShootEvent>()
                .OneFrame<NextFloorEvent>()
                .OneFrame<NextWaveEvent>()
                .OneFrame<SpawnEvent>()

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

    internal class RemoveSystem : Injects,IEcsRunSystem
    {
        private EcsFilter<DropData> _dropData;

        public void Run()
        {
            foreach (var item in _dropData)
            {
                ref var drop = ref _dropData.Get1(item);
                if (drop.weapon.AllAmunitionInInvent == 0 && drop.weapon.ammunition == 0)
                {
                    drop.time += Time.deltaTime;
                    
                }

                if (drop.time >= _staticData.DeleteDropTime)
                {
                    Object.Destroy(drop.collectActor.gameObject);
                    _dropData.GetEntity(item).Destroy();
                }
            }
        }
    }
}