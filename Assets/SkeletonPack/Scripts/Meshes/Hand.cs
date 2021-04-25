using UnityEngine;

namespace SkeletonEditor
{
    namespace Mesh
    {
        public class Hand : AbstractMesh
        {
            public Hand(Transform anchor, MeshType type) : base(anchor, type) {
            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/Hand " + (MeshType == MeshType.HandLeft ? 'L' : 'R');
            }
        }
    }

}

