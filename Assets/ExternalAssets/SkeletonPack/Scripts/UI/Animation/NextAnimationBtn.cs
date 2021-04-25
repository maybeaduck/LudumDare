using UnityEngine;
using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class NextAnimationBtn : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            PlayerController.Instance.OnNextAnimation();
        }
    }
}
