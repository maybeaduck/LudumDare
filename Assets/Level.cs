using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject[] Enemies;
    public int CountEnemies;

    public Transform[] SpawnTransforms;
    public bool Ready;

    private void Update()
    {
        if (Ready)
        {
            for (int i = 0; i < CountEnemies; i++)
            {
                var randomSpawnPoint = Random.Range(0, SpawnTransforms.Length);
                var randomEnemy = Random.Range(0, Enemies.Length);

                Instantiate(Enemies[randomEnemy],SpawnTransforms[randomSpawnPoint].position, SpawnTransforms[randomSpawnPoint].rotation);
            }

            Ready = false;
        }
    }
}
