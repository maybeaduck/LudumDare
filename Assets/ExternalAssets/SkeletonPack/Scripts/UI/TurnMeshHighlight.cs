using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnMeshHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image image;

    void Start() {
        image.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        image.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        image.enabled = false;
    }
}
