using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkeletonEditor
{
    public class ClearAllBtn : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private GameObject[] skinedMeshes;

        [EnumFlag]
        public TextureType textureMask;
        [EnumFlag]
        public FXType fxMask;
        [EnumFlag]
        public MeshType meshMask;

        private TextureType[] textures;
        private FXType[] fxMeshes;
        private MeshType[] meshes;

        public void Start()
        {
            PrepareTextureTypes();
            PrepareMeshTypes();
            PrepareFXTypes();
        }

        public void OnPointerClick(PointerEventData eventData) {
            TextureManager.Instance.OnClear(textures);
            MeshManager.Instance.OnClearMesh(meshes);
            MeshManager.Instance.OnClearFX(fxMeshes);
            for (int i = 0; i < skinedMeshes.Length; i++)
            {
                skinedMeshes[i].SetActive(false);
            }
        }

        protected void PrepareTextureTypes() {
            List<TextureType> list = new List<TextureType>();
            foreach (var enumValue in Enum.GetValues(typeof(TextureType))) {
                int checkBit = (int)textureMask & (int)enumValue;
                if (checkBit != 0)
                    list.Add((TextureType)enumValue);
            }
            textures = list.ToArray();
        }

        protected void PrepareFXTypes() {
            List<FXType> list = new List<FXType>();
            foreach (var enumValue in Enum.GetValues(typeof(FXType))) {
                int checkBit = (int)fxMask & (int)enumValue;
                if (checkBit != 0)
                    list.Add((FXType)enumValue);
            }
            fxMeshes = list.ToArray();
        }


        protected void PrepareMeshTypes() {
            List<MeshType> list = new List<MeshType>();
            foreach (var enumValue in Enum.GetValues(typeof(MeshType))) {
                int checkBit = (int)meshMask & (int)enumValue;
                if (checkBit != 0)
                    list.Add((MeshType)enumValue);
            }
            meshes = list.ToArray();
        }
    }
}
