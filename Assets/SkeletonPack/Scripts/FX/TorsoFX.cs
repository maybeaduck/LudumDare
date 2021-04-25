using UnityEngine;

namespace SkeletonEditor
{
    namespace FX
    {
        public class TorsoFX : AbstractFXMesh
        {
            public TorsoFX(Transform anchor) : base(anchor, FXType.Torso) {
            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/Torso FX";
            }
        }
    }

}

