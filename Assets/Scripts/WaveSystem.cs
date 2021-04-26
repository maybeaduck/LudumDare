using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;

namespace Zlodey
{
    public class WaveSystem : Injects, IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter<NextFloorEvent> _filter;
        private float _time;
        private float _floor;


        public void Init()
        {
            var timeToNextWave = _staticData.TimeToNextWave;
            _time = timeToNextWave + Time.time;

            _floor = _staticData.StartFloor;
            _ui.WaveScreen.FloorNumber.text = _floor.ToString();

            UpdateTimer();
        }

        public void Run()
        {
            var timeToNextWave = _staticData.TimeToNextWave;

            foreach (var item in _filter)
            {
                _time = timeToNextWave + Time.time;

                _floor++;
                _ui.WaveScreen.FloorNumber.text = _floor.ToString();
            }

            UpdateTimer();
        }

        void UpdateTimer()
        {
            var timeLeft = Mathf.Floor(_time - Time.time);
            if (timeLeft < 0) timeLeft = 0;

            _ui.WaveScreen.WaveTime.text = timeLeft.ToString();


            if (timeLeft == 0)
            {
                var timeToNextWave = _staticData.TimeToNextWave;
                _time = timeToNextWave + Time.time;

                _world.NewEntity().Get<SpawnEvent>();
            }
        }
    }


    public class SpawnSystem : Injects, IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter<SpawnEvent> _spawnFilter;
        private int _countEnemies;

        public void Init()
        {
            _countEnemies = _staticData.StartCountEnemies;
        }
        public void Run()
        {
            foreach (var item in _spawnFilter)
            {
                var spawnTransforms = _sceneData.Level.SpawnTransforms;
                var enemies = _staticData.Enemies;

                for (int i = 0; i < _countEnemies; i++)
                {
                    var randomSpawnPoint = Random.Range(0, spawnTransforms.Length);
                    var randomEnemy = Random.Range(0, enemies.Length);

                    GameObject.Instantiate(enemies[randomEnemy], spawnTransforms[randomSpawnPoint].position, spawnTransforms[randomSpawnPoint].rotation);
                }

                _countEnemies++;
            }
        }
    }
    public class NextFloorSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<NextFloorEvent> _nextFloorFilter;
        public void Run()
        {
            foreach (var item in _nextFloorFilter)
            {

                Debug.Log("_nextFloorFilter");
                var floors = _sceneData.Level.Floors;
                var time = _staticData.TimeToNextWave - 1f;

                foreach (var floor in floors)
                {
                    floor.transform.DOMoveY(floor.transform.position.y + 100f, time);

                    Debug.Log("floor");

                    if (floor.transform.position.y >= 100f)
                    {
                        var position = new Vector3(0f, -100f, 0f);
                        floor.transform.DOMoveY(-100f, 0f);
                    }
                }
            }
        }
    }

    public class CheckDeadEnemiesSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<EnemyData> _enemy;
        private EcsFilter<EnemyData, DieFlag> _deadEnemy;
        public void Run()
        {
            if (_enemy.GetEntitiesCount() > 1)
            {
                if (_enemy.GetEntitiesCount() == _deadEnemy.GetEntitiesCount())
                {
                    _world.NewEntity().Get<NextFloorEvent>();
                }
            }
        }
    }

    internal struct NextFloorEvent
    {
    }

    internal struct SpawnEvent
    {
    }
}