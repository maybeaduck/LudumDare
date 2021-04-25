using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageDestroy : MonoBehaviour
{
    public GameObject actor;
    public void Destroy()
    {
        Destroy(actor);
    }
}
