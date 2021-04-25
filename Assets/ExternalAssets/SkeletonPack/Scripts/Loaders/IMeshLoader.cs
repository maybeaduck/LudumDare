using SkeletonEditor.FX;
using SkeletonEditor.Mesh;
using UnityEngine;

namespace SkeletonEditor
{
    public interface IMeshLoader
    {
        void ParseMeshes(AbstractFXMesh mesh, out GameObject[] meshObjects);

        void ParseMeshes(AbstractMesh mesh, out GameObject[] meshObjects);

    }
}
