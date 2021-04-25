using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class PrevColorBtn : TextureTypeMaskSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            TextureManager.Instance.OnPrevColor(types);
        }
    }
}
