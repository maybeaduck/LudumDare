using UnityEngine;

namespace SkeletonEditor
{
    namespace Mesh
    {
        public class Shoulder : AbstractMesh
        {
            public Shoulder(Transform anchor, MeshType meshType) : base(anchor, meshType) {
            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/Shoulder " + (MeshType == MeshType.ShoulderLeft ? 'L' : 'R');
            }
        }
    }

}

