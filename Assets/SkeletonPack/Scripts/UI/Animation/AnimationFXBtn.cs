using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class AnimationFXBtn : MonoBehaviour, IPointerClickHandler
    {
        public AppearAnimationType animationType;
        public ParticleSystem animationFX;

        public void OnPointerClick(PointerEventData eventData) {

            PlayerController.Instance.PlayAnimation(animationType);
            animationFX.Play();
        }
    }
}
