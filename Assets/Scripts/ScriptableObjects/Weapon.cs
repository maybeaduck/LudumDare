using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

public enum WeaponType
{
    Weapon,
    Ak,
    Sniper,
    FiveSeven,
    FireGun,
    DesertEagle,
    MashineGun,
    BombGun,
    ShotGun
}
namespace Zlodey
{
    public class Weapon : MonoBehaviour
    {
        public WeaponType WeaponType;
        public Transform ShootPoint;
        
        public float bulletSpeed;
        public float MinDamage;
        public float MaxDamage;
        public float spread;
        public int ammunition;
        public int defaultAmunition;
        public float reloadTime;
        public float shotsTime;
        public int AllAmunitionInInvent;
        
        public int countShot;
        public string BulletType;
        public float MaxLiveTime;
        public float BoomSize;
        public float BoomSpeed;
        
    }
}