using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;
using Zlodey;

public class Level : MonoBehaviour
{
    public Transform[] SpawnTransforms;
    public GameObject[] Floors;
    public EnemyActor[] Enemies;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Service<EcsWorld>.Get().NewEntity().Get<NextFloorEvent>();
        }
    }
}