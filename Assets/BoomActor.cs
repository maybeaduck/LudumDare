using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomActor : MonoBehaviour
{
    public SphereCollider BoomColider;

    private void OnDrawGizmos()
    {
        // Gizmos.DrawSphere(transform.position,BoomColider.radius);
    }
}