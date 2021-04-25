using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageDestroy : MonoBehaviour
{
    public BandageActor actor;
    public void Destroy()
    {
        Destroy(actor.gameObject);
    }
}
