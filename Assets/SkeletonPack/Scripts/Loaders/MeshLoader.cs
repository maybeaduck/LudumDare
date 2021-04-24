using UnityEngine;
using System.IO;
using UnityEditor;
using SkeletonEditor.FX;
using SkeletonEditor.Mesh;

namespace SkeletonEditor
{

    public class MeshLoader : IMeshLoader
    {
        public void ParseMeshes(AbstractFXMesh mesh, out GameObject[] meshObjects)
        {
            var objects = AssetDatabase.FindAssets("t:GameObject", new string[]
                {
                    mesh.GetFolderPath()
                }
            );

            meshObjects = new GameObject[objects.Length];
            for (int i = 0; i < objects.Length; i++) {
                meshObjects[i] = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(objects[i]), typeof(GameObject)) as GameObject;
            }
        }

        public void ParseMeshes(AbstractMesh mesh, out GameObject[] meshObjects) {

            var meshGUIDs = AssetDatabase.FindAssets("t:GameObject", new string[]
                {
                    mesh.GetFolderPath()
                }
            );
            meshObjects = new GameObject[meshGUIDs.Length];

            for (int i = 0; i < meshGUIDs.Length; i++) {
                meshObjects[i] = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(meshGUIDs[i]), typeof(GameObject)) as GameObject;
            }
        }
    }
}
