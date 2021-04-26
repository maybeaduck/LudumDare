using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SkeletonEditor
{

    public class TextureMergeComparer : IComparer<AbstractTexture>
    {
        public int Compare(AbstractTexture x, AbstractTexture y) {
            return (new CaseInsensitiveComparer()).Compare(x.MergeOrder, y.MergeOrder);
        }
    }


    public class TextureManager : MonoBehaviour
    {
        public Renderer[] modelRenderers;

        private string characterRace;
        private AbstractTexture baseTexture;

        private Color32[] basedPixels;
        private Color32[] changedPixels;

        public Dictionary<TextureType, AbstractTexture> skinTextures {
            get; private set;
        }

        private AbstractTexture[] sortTextures;

        private Texture2D mergedTexture;

        private TextureType[] ignoreTypes;

        public static TextureManager Instance { get; private set; }

        void Awake() {
            if (Instance != null) {
                Destroy(this.gameObject);
            }
            Instance = this;
        }

        void Start() {
            ITextureLoader loader = new TextureLoader();

            skinTextures = new Dictionary<TextureType, AbstractTexture>
            {
                { TextureType.Skin, new Textures.Skin(loader, 0, 0, 2048, 2048)},
            };

            basedPixels = changedPixels = new Color32[1024*1024];

            sortTextures = skinTextures.Values.ToArray();
            Array.Sort(sortTextures, new TextureMergeComparer());
        }

        public void MergeTextures()
        {
            if (sortTextures.Length > 1) {
                basedPixels = sortTextures[0].GetPixels32();
                Array.Copy(basedPixels, changedPixels, basedPixels.Length);

                int index;
                for (int i = 1; i < sortTextures.Length; i++) {
                    if (ignoreTypes != null && ignoreTypes.Contains(sortTextures[i].Type)) continue; 

                    var currentPixels = sortTextures[i].GetPixels32();
                    for (int j = 0; j < currentPixels.Length; j++)
                    {
                        if (currentPixels[j].a != 0)
                        {
                            index = (sortTextures[0].width * sortTextures[i].y + sortTextures[i].x) + j + ((int) j / sortTextures[i].width) * (sortTextures[0].width - sortTextures[i].width);
                            changedPixels[index] = Color32.LerpUnclamped(changedPixels[index], currentPixels[j], currentPixels[j].a / 255f);
                        }
                    }
                }

                mergedTexture = new Texture2D(sortTextures[0].width, sortTextures[0].height);
                mergedTexture.SetPixels32(changedPixels);
                mergedTexture.Apply();
            }
            else {
                mergedTexture = sortTextures[0].Current;
            }
            UpdateTexture(mergedTexture);
            ignoreTypes = null;
        }


        public Texture2D GetMergedTexture() {
            return mergedTexture;
        }

        public void UpdateMaterial(Material mat) {
            for (var i = 0; i < modelRenderers.Length; i++) {
                modelRenderers[i].material = mat;
            }
        }

        public void UpdateTexture(Texture2D texture) {
            for (var i = 0; i < modelRenderers.Length; i++) {
                modelRenderers[i].material.mainTexture = texture;
            }
        }


        public void OnPrevTexture(TextureType[] types) {
            for (int i = 0; i < types.Length; i++) {
                if (i == 0) {
                    skinTextures[types[i]].MovePrev();
                }
                else {
                    skinTextures[types[i]].SelectedTexture = skinTextures[types[i - 1]].SelectedTexture;
                }
            }
            OnChangeTexture(types);
        }

        public void OnPrevColor(TextureType[] types) {
            for (int i = 0; i < types.Length; i++) {
                if (i == 0) {
                    skinTextures[types[i]].MovePrevColor();
                }
                else {
                    skinTextures[types[i]].SelectedColor = skinTextures[types[i - 1]].SelectedColor;
                }
            }
            OnChangeTexture(types);
        }

        public void OnNextTexture(TextureType[] types) {
            for (int i = 0; i < types.Length; i++) {
                if (i == 0) {
                    skinTextures[types[i]].MoveNext();
                }
                else {
                    skinTextures[types[i]].SelectedTexture = skinTextures[types[i - 1]].SelectedTexture;
                }
            }
            OnChangeTexture(types);
        }

        public void OnNextColor(TextureType[] types) {
            for (int i = 0; i < types.Length; i++) {
                if (i == 0) {
                    skinTextures[types[i]].MoveNextColor();
                }
                else {
                    skinTextures[types[i]].SelectedColor = skinTextures[types[i - 1]].SelectedColor;
                }
            }
            OnChangeTexture(types);
        }

        public void OnResetTexure(TextureType[] types) {
            for (int i = 0; i < types.Length; i++) {
                skinTextures[types[i]].Reset();
            }
            OnChangeTexture(types);
        }

        public void OnResetColor(TextureType[] types) {
            for (int i = 0; i < types.Length; i++) {
                skinTextures[types[i]].ResetColor();
            }
            OnChangeTexture(types);
        }

        public void OnClear(TextureType[] types) {
            for (int i = 0; i < types.Length; i++) {
                skinTextures[types[i]].ResetColor();
                skinTextures[types[i]].Reset();
            }
            OnChangeTexture(types);
        }

        public void OnRandom(TextureType[] types, TextureType[] sameColors, TextureType[] ignoreTypes = null) {
            for (int i = 0; i < types.Length; i++) {
                skinTextures[types[i]].Shuffle();
                skinTextures[types[i]].ShuffleColor();
            }
            for (int i = 1; i < sameColors.Length; i++) {
                skinTextures[sameColors[i]].SelectedColor = skinTextures[sameColors[0]].SelectedColor;
            }
            this.ignoreTypes = ignoreTypes;
            OnChangeTexture(types);
        }

        private void OnChangeTexture(TextureType[] types)
        {
            if (types == null || types.Length == 0)
                return;

            MergeTextures();
        }
    }
}
