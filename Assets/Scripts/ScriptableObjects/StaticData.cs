using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Zlodey
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [Header("Props")]
        public Levels Levels;
        public ObjectPoolController ObjectPooler;
        [Header("Prefabs")]
        public AudioSource AudioSourcePrefab;
        public UI UIPrefab;
        public float speed;
        public float speedRotation;
        public float TestDamage;

        [Header("Bullet")]
        public Bullet Bullet;
        public float BulletSpeed;
        public float BulletCooldownTime;
        public float Visota;

        public GameObject ShootFX;
        public GameObject HitEnemyFX;
        public GameObject HitWallFX;

        public Color CritColorText;
        public float DamageCritValue;

        [Header("Dash")]
        public float DashCooldownTime;
        public float DashSpeed;
        public float sceletonDistanceToPlayer;
        public float sceletonDistanceBackDistance;
        public List<WeaponCollectActor> allWeapons = new List<WeaponCollectActor>();

        [Header("Level")]
        public float TimeToNextWave;
        public int StartFloor;

        [Header("Enemies")]
        public GameObject[] Enemies;
        public int StartCountEnemies;

        public List<drop> DropItems = new List<drop>();

        public float DeleteTrupsTime;
        public float DeleteDropTime;

        [Header("Cursor")]

        [Header("Weapons images")]
        public Sprite AkSprite;
        public Sprite BombGunSprite;
        public Sprite DesertEagleSprite;
        public Sprite FireGunSprite;
        public Sprite FiveSevenSprite;
        public Sprite MashinePistolSprite;
        public Sprite ShotGunSprite;
        public Sprite SniperGunSprite;
    }
    [Serializable]
    public class drop
    {
        public GameObject DropItem;
        [Range(0, 1)] public float chance;
    }
}