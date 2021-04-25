using UnityEngine;

namespace SkeletonEditor
{
    namespace Mesh
    {
        public class Helm : AbstractMesh
        {
            public Helm(Transform anchor) : base(anchor, MeshType.Helm) {
            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/Head";
            }
        }
    }

}

