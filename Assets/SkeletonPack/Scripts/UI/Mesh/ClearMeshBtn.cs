using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class ClearMeshBtn : MeshTypeMaskSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            MeshManager.Instance.OnClearMesh(types);
        }
    }
}
