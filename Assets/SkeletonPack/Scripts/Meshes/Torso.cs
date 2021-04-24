using UnityEngine;

namespace SkeletonEditor
{
    namespace Mesh
    {
        public class Torso : AbstractMesh
        {
            public Torso(Transform anchor) : base(anchor, MeshType.Torso) {
            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/Torso";
            }
        }
    }

}

