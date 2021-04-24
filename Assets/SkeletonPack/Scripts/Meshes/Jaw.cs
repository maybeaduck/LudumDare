using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonEditor
{
    namespace Mesh
    {
        public class Jaw : AbstractMesh
        {
            public Jaw(Transform anchor):base(anchor, MeshType.Jaw)
            {
               
            }

            protected override void InstantiateMeshes(GameObject[] mesheobjects) {
                var meshModels = new List<Renderer>(TextureManager.Instance.modelRenderers);

                for (int i = 0; i < MeshesCount; i++) {
                    meshes[i] = UnityEngine.Object.Instantiate(mesheobjects[i]);
                    meshes[i].transform.SetParent(parent.transform);
                    meshes[i].SetActive(false);

                    MeshRenderer render = mesheobjects[i].GetComponent<MeshRenderer>();
                    if (render.sharedMaterial.mainTexture.Equals(meshModels[0].material.mainTexture)) {
                        meshModels.Add(meshes[i].GetComponent<MeshRenderer>());
                    }
                }
                TextureManager.Instance.modelRenderers = meshModels.ToArray();

            }

            public override string GetFolderPath() {
                return "Assets/SkeletonPack/Prefabs/Skeleton/Jaw";
            }
        }
    }

}

