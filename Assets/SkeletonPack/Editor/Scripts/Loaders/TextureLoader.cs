using System.IO;
using UnityEditor;
using UnityEngine;

namespace SkeletonEditor
{
    public class TextureLoader : ITextureLoader
    {
        public Texture2D LoadTexture(string path) {
            return AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D)) as Texture2D;
        }

        public void UnloadTextures() {
            EditorUtility.UnloadUnusedAssetsImmediate();
        }

        public string[][] ParseTextures(AbstractTexture texture)
        {
            return ParseTextures(texture.GetFolderPath());
        }

        public string[][] ParseTextures(string path) {
            var folders = Directory.GetDirectories(Path.Combine(Application.dataPath, path.Substring(7)));

            bool withColor = folders.Length != 0;

            string[][] texturePaths;

            if (withColor) {
                texturePaths = new string[folders.Length][];

                for (int i = 0; i < folders.Length; i++) {
                    var textures = AssetDatabase.FindAssets("t:texture2D", new string[]
                        {
                            folders[i].Substring(Application.dataPath.Length - 6)
                        }
                    );
                    texturePaths[i] = new string[textures.Length];
                    for (int j = 0; j < textures.Length; j++) {
                        texturePaths[i][j] = AssetDatabase.GUIDToAssetPath(textures[j]);
                    }
                }
            }
            else {

                var textures = AssetDatabase.FindAssets("t:texture2D", new string[] { path });
                texturePaths = new string[textures.Length][];

                for (int i = 0; i < textures.Length; i++) {
                    texturePaths[i] = new string[] { AssetDatabase.GUIDToAssetPath(textures[i]) };
                }
            }
            return texturePaths;
        }
    }
}
