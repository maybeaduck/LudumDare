using UnityEngine;
using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class PrevAnimationBtn : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            PlayerController.Instance.OnPrevAnimation();
        }
    }
}
