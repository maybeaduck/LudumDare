using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;


namespace SkeletonEditor
{
    public class SaveManager : MonoBehaviour
    {
        public GameObject meshModel;

        public static SaveManager Instance { get; private set; }

        void Awake() {
            if (Instance != null) {
                Destroy(this.gameObject);
            }
            Instance = this;
        }

        public void OnSaveClick() {
            SavePrefab();
        }

        protected void SavePrefab() {
            string prefabNum = System.DateTime.Now.ToString("yyyyMMddHHmmssfff");

            AssetDatabase.CreateFolder("Assets/SkeletonPack/NewCharacter", "Skeleton_" + prefabNum);

            string folderName = "Assets/SkeletonPack/NewCharacter/Skeleton_" + prefabNum;
            CreateSkinAtlas(folderName);

        }

        protected void CreateSkinAtlas(string folderName) {

            Profiler.BeginSample("START SAVE");
            var materialSkin = new Material(TextureManager.Instance.modelRenderers[0].material.shader);

            materialSkin.mainTexture = TextureManager.Instance.GetMergedTexture();
            Profiler.EndSample();
            Profiler.BeginSample("START SAVE 2 ");
            AssetDatabase.CreateAsset(materialSkin, folderName + "/SkeletonSkin.mat");
            Profiler.EndSample();
            Profiler.BeginSample("START SAVE 3 ");
            AssetDatabase.Refresh();
            Profiler.EndSample();
            Profiler.BeginSample("START SAVE 4 ");
            TextureManager.Instance.UpdateMaterial(materialSkin);

            PrefabUtility.CreatePrefab(folderName + "/Skeleton.prefab", meshModel, ReplacePrefabOptions.Default);
            Profiler.EndSample();
        }



    }
}
