using System.Collections.Generic;
using UnityEngine;

namespace SkeletonEditor
{
    public class TextureTypeMaskSelector : MonoBehaviour
    {
        [EnumFlag]
        public TextureType typeMask;
        protected TextureType[] types;

        public void Start()
        {
            List<TextureType> list = new List<TextureType>();
            foreach (var enumValue in System.Enum.GetValues(typeof(TextureType)))
            {
                int checkBit = (int)typeMask & (int) enumValue;
                if (checkBit != 0)
                    list.Add((TextureType) enumValue);
            }
            types = list.ToArray();
        }
    }
}