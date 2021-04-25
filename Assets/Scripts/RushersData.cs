using UnityEngine;
using UnityEngine.AI;

namespace Zlodey
{
    internal struct RushersData
    {
        public NavMeshAgent meshAgent;
        public float botSpeed;
        public Collider botFilter;
        public Collider AttackZone;
    }
}