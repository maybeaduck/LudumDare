using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    public float TimeToDestroy = 0.2f;

    void Update()
    {
        Destroy(gameObject, TimeToDestroy);
    }
}
