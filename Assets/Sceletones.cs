using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zlodey;

public class Sceletones : MonoBehaviour
{
    public GameObject AtackHand;
    public Health health;
    private void Update()
    {
        if (health.Value >= 0)
        {
            AtackHand.SetActive(false);
        }
    }
}
