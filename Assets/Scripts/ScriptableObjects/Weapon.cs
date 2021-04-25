using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Zlodey
{
    public class Weapon : MonoBehaviour
    {
        public Transform ShootPoint;
        public Transform WeaponModel;
        public float MinDamage;
        public float MaxDamage;
        public float spread;
        public int ammunition;
        public int defaultAmunition;
        public float reloadTime;
        public float shotsTime;
        public int AllAmunitionInInvent;
        public int DefaultAmunitionInInvent;
        public int countShot;
        public string BulletType;
    }
}