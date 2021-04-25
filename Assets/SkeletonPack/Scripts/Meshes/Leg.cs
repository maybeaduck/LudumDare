using UnityEngine;

namespace SkeletonEditor
{
    namespace Mesh
    {
        public class Leg : AbstractMesh
        {
            public Leg(Transform anchor, MeshType type) : base(anchor, type) {
            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/Leg " + (MeshType == MeshType.LegLeft ? 'L' : 'R');
            }
        }
    }

}

