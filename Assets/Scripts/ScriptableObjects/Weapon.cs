using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Zlodey
{
    public class Weapon : MonoBehaviour
    {
        public Transform ShootPoint;
        public Transform WeaponModel;
        public float Damage;
        public float spread;
        public int ammunition;
        public int defaultAmunition;
        public float reloadTime;
        public int AllAmunitionInInvent;
        public int DefaultAmunitionInInvent;
    }
}