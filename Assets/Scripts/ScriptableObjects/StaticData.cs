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

        [Header("Dash")]
        public float DashCooldownTime;
        public float DashSpeed;
    }
}