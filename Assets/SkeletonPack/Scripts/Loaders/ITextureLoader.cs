using UnityEngine;

namespace SkeletonEditor
{
    public interface ITextureLoader
    {
        Texture2D LoadTexture(string path);

        void UnloadTextures();

        string[][] ParseTextures(string path);
        string[][] ParseTextures(AbstractTexture path);

    }
}
