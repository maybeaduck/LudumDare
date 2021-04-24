using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class NextFXBtn : FXTypeMaskSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            MeshManager.Instance.OnNextFX(types);
        }
    }
}
