using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class NextColorBtn : TextureTypeMaskSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            TextureManager.Instance.OnNextColor(types);
        }
    }
}
