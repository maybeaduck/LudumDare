using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class PrevMeshBtn : MeshTypeMaskSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            MeshManager.Instance.OnPrevMesh(types);
        }
    }
}
