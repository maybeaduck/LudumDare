using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Zlodey
{
    public class WaveSystem : Injects, IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter<NextWaveEvent> _filter;
        private float _time;

        public void Init()
        {
            SetTimer();
            UpdateTimer();
        }

        public void Run()
        {
            foreach (var item in _filter)
            {
                SetTimer();
            }

            UpdateTimer();
        }
        void SetTimer()
        {
            var timeToNextWave = _staticData.TimeToNextWave;
            _time = timeToNextWave + Time.time;
        }

        void UpdateTimer()
        {
            var timeLeft = Mathf.Floor(_time - Time.time);
            if (timeLeft < 0) timeLeft = 0;

            _ui.WaveScreen.WaveTime.text = timeLeft.ToString();

            var isActive = timeLeft != 0 ? true : false;

            //check
            if (timeLeft == 0 && _ui.WaveScreen.WaveTime.gameObject.activeSelf)
            {
                _world.NewEntity().Get<SpawnEvent>();
            }

            _ui.WaveScreen.WaveTime.gameObject.SetActive(isActive);
        }
    }
    public class WeaponUIUpdateSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<PersonData> _person;
        public void Run()
        {
            foreach (var item in _person)
            {
                ref var weapon = ref _person.Get1(item).Weapon;

                if (!weapon) return;

                Sprite sprite;
                    switch (weapon.name)
                    {
                        case "Ak":
                            sprite = _staticData.AkSprite;
                            break;
                        case "BombGun":
                            sprite = _staticData.BombGunSprite;
                            break;
                        case "DesertEagle":
                            sprite = _staticData.DesertEagleSprite;
                            break;
                        case "FireGun":
                            sprite = _staticData.FireGunSprite;
                            break;
                        case "FiveSeven":
                            sprite = _staticData.FiveSevenSprite;
                            break;
                        case "MashinePistol":
                            sprite = _staticData.MashinePistolSprite;
                            break;
                        case "ShotGun":
                            sprite = _staticData.ShotGunSprite;
                            break;
                        case "SniperGun":
                            sprite = _staticData.SniperGunSprite;
                            break;

                        default:
                            sprite = _staticData.SniperGunSprite;
                            break;
                    }

                    _ui.WaveScreen.WeaponImage.sprite = sprite;
                    _ui.WaveScreen.AammoValue.text = weapon.ammunition.ToString();
                    _ui.WaveScreen.AmmoValueMax.text = weapon.defaultAmunition.ToString();
                
            }
        }
    }

    internal struct NextWaveEvent
    {
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
    public class NextFloorSystem : Injects, IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter<NextFloorEvent> _nextFloorFilter;
        private float _floor;
        public void Init()
        {
            _floor = _staticData.StartFloor;
            _ui.WaveScreen.FloorNumber.text = _floor.ToString();
        }

        public void Run()
        {
            foreach (var item in _nextFloorFilter)
            {
                _floor++;
                _ui.WaveScreen.FloorNumber.text = _floor.ToString();

                var floors = _sceneData.Level.Floors;
                var time = _staticData.TimeToNextWave - 1f;

                foreach (var floor in floors)
                {

                    if (floor.transform.position.y >= 100f)
                    {
                        var position = new Vector3(0f, -100f, 0f);
                        floor.transform.position = position;
                    }

                    floor.transform.DOMoveY(floor.transform.position.y + 100f, time);
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
            if (_enemy.GetEntitiesCount() > 0)
            {
                if (_enemy.GetEntitiesCount() == _deadEnemy.GetEntitiesCount())
                {
                    _world.NewEntity().Get<NextFloorEvent>();
                    _world.NewEntity().Get<NextWaveEvent>();

                    foreach (var item in _enemy)
                    {
                        _enemy.GetEntity(item).Destroy();
                    }
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