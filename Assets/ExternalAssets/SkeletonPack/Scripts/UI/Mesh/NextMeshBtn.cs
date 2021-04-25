using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class NextMeshBtn : MeshTypeMaskSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            MeshManager.Instance.OnNextMesh(types);
        }
    }
}
