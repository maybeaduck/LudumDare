namespace SkeletonEditor
{
    namespace Textures
    {
        public class Skin : AbstractTexture
        {
            public Skin(ITextureLoader loader, int x, int y, int width, int height) :
                base(loader, x, y, width, height, 0, TextureType.Skin)
            {
            }

            public override string GetFolderPath()
            {
                return "Assets/SkeletonPack/Textures/Character/Skin";
            }
        }
    }
}

