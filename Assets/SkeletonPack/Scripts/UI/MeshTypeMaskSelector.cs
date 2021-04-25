using System.Collections.Generic;
using UnityEngine;

namespace SkeletonEditor
{
    public class MeshTypeMaskSelector : MonoBehaviour
    {
        [EnumFlag]
        public MeshType typeMask;
        protected MeshType[] types;

        public void Start() {
            List<MeshType> list = new List<MeshType>();
            foreach (var enumValue in System.Enum.GetValues(typeof(MeshType))) {
                int checkBit = (int)typeMask & (int)enumValue;
                if (checkBit != 0)
                    list.Add((MeshType)enumValue);
            }
            types = list.ToArray();
        }
    }
}