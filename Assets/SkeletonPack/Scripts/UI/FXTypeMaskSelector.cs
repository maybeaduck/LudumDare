using System.Collections.Generic;
using UnityEngine;

namespace SkeletonEditor
{
    public class FXTypeMaskSelector : MonoBehaviour
    {
        [EnumFlag]
        public FXType typeMask;
        protected FXType[] types;

        public void Start() {
            List<FXType> list = new List<FXType>();
            foreach (var enumValue in System.Enum.GetValues(typeof(FXType))) {
                int checkBit = (int)typeMask & (int)enumValue;
                if (checkBit != 0)
                    list.Add((FXType)enumValue);
            }
            types = list.ToArray();
        }
    }
}