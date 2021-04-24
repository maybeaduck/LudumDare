using UnityEngine;

namespace SkeletonEditor
{
    namespace FX
    {
        public class EyeFX : AbstractFXMesh
        {
            public EyeFX(Transform anchor) : base(anchor, FXType.Eye) {
            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/Eye FX";
            }
        }
    }

}

