using UnityEngine;

namespace SkeletonEditor
{
    namespace Mesh
    {
        public class Belt : AbstractMesh
        {
            public Belt(Transform anchor) : base(anchor, MeshType.Belt) {
            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/Belt";
            }
        }
    }

}

