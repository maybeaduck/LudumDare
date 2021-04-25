using System.Collections.Generic;
using UnityEngine;

namespace SkeletonEditor
{
    public abstract class AbstractTexture
    {
        public readonly int x;
        public readonly int y;
        public readonly int width;
        public readonly int height;

        public readonly TextureType Type;
        public readonly int MergeOrder;


        private readonly ITextureLoader textureLoader;
        private readonly string[][] textures;

        private int _selectedColor;
        private int _selectedTexture;

        public int SelectedTexture
        {
            get { return _selectedTexture; }
            set
            {
                if (value >= textures.Length) {
                    value = 0;
                }
                else if (value < 0) {
                    value = textures.Length - 1;
                }
                _selectedTexture = value;
            }
        }

        public int SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                if (value >= textures[_selectedTexture].Length) {
                    value = 0;
                }
                else if (value < 0) {
                    value = textures[_selectedTexture].Length - 1;
                }
                _selectedColor = value;
            }
        }


        public Texture2D Current
        {
            get
            {
                var path = textures[_selectedTexture][_selectedColor];
                if (!textureCache.ContainsKey(path)) {
                    textureCache[path] = textureLoader.LoadTexture(path);
                }
                return textureCache[path];
            }
        }

        private Dictionary<string, Color32[]> cache = new Dictionary<string, Color32[]>();
        private Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

        public AbstractTexture(ITextureLoader loader, int width, int height, string path) {
            this.textureLoader = loader;
            this.height = height;
            this.width = width;

            textures = textureLoader.ParseTextures(path);
        }

        public AbstractTexture(ITextureLoader loader, int x, int y, int width, int height, int order, TextureType type = TextureType.Skin) {
            this.textureLoader = loader;

            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            this.MergeOrder = order;
            this.Type = type;

            textures = textureLoader.ParseTextures(this);
        }

        public abstract string GetFolderPath();


        public void MoveNext() {
            SelectedTexture++;
        }

        public bool HasNext() {
            return SelectedTexture != textures.Length - 1;
        }

        public void MovePrev() {
            SelectedTexture--;
        }

        public bool HasPrev() {
            return SelectedTexture != 0;
        }

        public void Reset() {
            SelectedTexture = 0;
        }

        public void Shuffle() {
            SelectedTexture = UnityEngine.Random.Range(0, textures.Length);
        }

        public void MoveNextColor() {
            SelectedColor++;
        }

        public void MovePrevColor() {
            SelectedColor--;
        }

        public void ResetColor() {
            SelectedColor = 0;
        }

        public void ShuffleColor() {
            SelectedColor = UnityEngine.Random.Range(0, textures[SelectedTexture].Length);
        }

        public Color32[] GetPixels32() {
            string key = textures[_selectedTexture][_selectedColor];

            if (!cache.ContainsKey(key)) {
                cache[key] = Current.GetPixels32();
                textureLoader.UnloadTextures();
            }

            return cache[key];
        }
    }
}