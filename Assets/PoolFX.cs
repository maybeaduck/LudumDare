using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolFX : MonoBehaviour, IPooledObject
{
    public bool offai;
    public Animator animator;
    public ParticleSystem particleSystem;
    public DamageFX DamageFX;
    public void OnObjectSpawn()
    {
        
    }

    public void SetPool(Queue<GameObject> pool)
    {
        if (offai)
        {
            gameObject.SetActive(false);     
        }
       
    }
}
