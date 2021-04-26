using UnityEngine;
using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class SavePrefabBtn : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            SaveManager.Instance.OnSaveClick();
        }
    }
}