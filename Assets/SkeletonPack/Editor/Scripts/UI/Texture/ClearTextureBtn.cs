using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class ClearTextureBtn : TextureTypeMaskSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            TextureManager.Instance.OnResetTexure(types);
        }
    }
}
