using UnityEngine;

namespace SkeletonEditor
{
    namespace Mesh
    {
        public class TorsoAdd : AbstractMesh
        {
            public TorsoAdd(Transform anchor) : base(anchor, MeshType.TorsoAdd) {
            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/TorsoAdd";
            }
        }
    }

}

