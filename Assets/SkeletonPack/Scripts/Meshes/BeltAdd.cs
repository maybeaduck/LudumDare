using UnityEngine;

namespace SkeletonEditor
{
    namespace Mesh
    {
        public class BeltAdd : AbstractMesh
        {
            public BeltAdd(Transform anchor) : base(anchor, MeshType.BeltAdd) {
            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/BeltAdd";
            }
        }
    }

}

