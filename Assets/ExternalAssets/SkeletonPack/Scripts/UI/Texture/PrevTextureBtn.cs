using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class PrevTextureBtn : TextureTypeMaskSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            TextureManager.Instance.OnPrevTexture(types);
        }
    }
}
