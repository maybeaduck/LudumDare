using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class ClearColorBtn : TextureTypeMaskSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            TextureManager.Instance.OnResetColor(types);
        }
    }
}
