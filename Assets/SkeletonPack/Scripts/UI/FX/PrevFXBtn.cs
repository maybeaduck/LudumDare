using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class PrevFXBtn : FXTypeMaskSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            MeshManager.Instance.OnPrevFX(types);
        }
    }
}
