using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class NextTextureBtn : TextureTypeMaskSelector, IPointerClickHandler
    {
        
        public void OnPointerClick(PointerEventData eventData) {
            TextureManager.Instance.OnNextTexture(types);
        }
    }
}
