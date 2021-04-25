using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class ClearFXBtn : FXTypeMaskSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            MeshManager.Instance.OnClearFX(types);
        }
    }
}
