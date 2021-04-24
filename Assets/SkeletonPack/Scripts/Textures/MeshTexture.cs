namespace SkeletonEditor
{
    public class MeshTexture : AbstractTexture
    {
        private readonly string folderPath;

        public MeshTexture(ITextureLoader loader, string path) :
            base(loader, 512, 512, path)
        {
            folderPath = path;
        }

        public override string GetFolderPath()
        {
            return folderPath;
        }
    }
}
