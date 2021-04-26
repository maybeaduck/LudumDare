using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowToMouse : MonoBehaviour
{
    public RectTransform RectTransform;
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
