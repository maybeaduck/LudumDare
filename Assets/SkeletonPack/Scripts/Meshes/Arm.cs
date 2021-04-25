using UnityEngine;

namespace SkeletonEditor
{
    namespace Mesh
    {
        public class Arm : AbstractMesh
        {
            public Arm(Transform anchor, MeshType type) : base(anchor, type) {
            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/Arm " + (MeshType == MeshType.ArmLeft ? 'L' : 'R');
            }
        }
    }

}

