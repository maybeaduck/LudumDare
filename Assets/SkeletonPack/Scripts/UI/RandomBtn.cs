﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkeletonEditor
{

    [Serializable]
    public class TypeMask
    {
        [EnumFlag]
        public MeshType types;
    }

    public class RandomBtn : MonoBehaviour, IPointerClickHandler
    {

        [Header("Mesh settings")]
        [EnumFlag]
        public MeshType meshTypeMask;
        public TypeMask[] sameMeshes;

        private MeshType[] randomMesheTypes;
        private MeshType[][] sameMesheTypes;

        [Header("Texture settings")]
        [EnumFlag]
        public TextureType textureTypeMask;
        private TextureType[] randomTextureTypes;
        private TextureType[] ignoreTextureTypes;

        [Header("Color settings")]
        [EnumFlag]
        public MeshType colorMeshTypeMask;
        [EnumFlag]
        public TextureType colorTextureTypeMask;

        private MeshType[] sameMeshColorTypes;
        private TextureType[] sameTextureColorTypes;

        [Header("FX settings")]
        [EnumFlag]
        public FXType fxTypeMask;
        private FXType[] randomFxTypes;


        public void Start()
        {
            PrepareTextureTypes();
            PrepareMeshTypes();
            PrepareFXTypes();
            PrepareSameColorTypes();
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            MeshManager.Instance.OnRandom(randomMesheTypes, sameMesheTypes);
            MeshManager.Instance.OnRandomFX(randomFxTypes);
            TextureManager.Instance.OnRandom(randomTextureTypes, sameTextureColorTypes, ignoreTextureTypes);
        }

        protected void PrepareTextureTypes()
        {
            List<TextureType> list = new List<TextureType>();
            foreach (var enumValue in System.Enum.GetValues(typeof(TextureType))) {
                int checkBit = (int)textureTypeMask & (int)enumValue;
                if (checkBit != 0)
                    list.Add((TextureType)enumValue);
            }
            randomTextureTypes = list.ToArray();
        }

        protected void PrepareFXTypes()
        {
            List<FXType> list = new List<FXType>();
            foreach (var enumValue in System.Enum.GetValues(typeof(FXType))) {
                int checkBit = (int)fxTypeMask & (int)enumValue;
                if (checkBit != 0)
                    list.Add((FXType)enumValue);
            }
            randomFxTypes = list.ToArray();
        }

        protected void PrepareSameColorTypes()
        {
            List<MeshType> meshList = new List<MeshType>();
            foreach (var enumValue in Enum.GetValues(typeof(MeshType))) {
                int checkBit = (int)colorMeshTypeMask & (int)enumValue;
                if (checkBit != 0)
                    meshList.Add((MeshType)enumValue);
            }
            sameMeshColorTypes = meshList.ToArray();

            List<TextureType> textureList = new List<TextureType>();
            foreach (var enumValue in Enum.GetValues(typeof(TextureType))) {
                int checkBit = (int)colorTextureTypeMask & (int)enumValue;
                if (checkBit != 0)
                    textureList.Add((TextureType)enumValue);
            }
            sameTextureColorTypes = textureList.ToArray();
        }

        protected void PrepareMeshTypes()
        {
            List<MeshType> list = new List<MeshType>();
            foreach (var enumValue in Enum.GetValues(typeof(MeshType))) {
                int checkBit = (int)meshTypeMask & (int)enumValue;
                if (checkBit != 0)
                    list.Add((MeshType)enumValue);
            }
            randomMesheTypes = list.ToArray();

            List<MeshType> sameList = new List<MeshType>();
            sameMesheTypes = new MeshType[sameMeshes.Length][];
            for (int i = 0; i < sameMeshes.Length; i++) {
                sameList.Clear();
                foreach (var enumValue in Enum.GetValues(typeof(MeshType))) {
                    int checkBit = (int)sameMeshes[i].types & (int)enumValue;
                    if (checkBit != 0)
                        sameList.Add((MeshType)enumValue);
                }
                sameMesheTypes[i] = sameList.ToArray();
            }
        }
    }
}