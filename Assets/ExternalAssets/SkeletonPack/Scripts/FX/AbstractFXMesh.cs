using UnityEngine;

namespace SkeletonEditor
{
    namespace FX
    {
        public abstract class AbstractFXMesh
        {
            private GameObject parent;

            protected Transform anchor;

            protected FXType fxType;

            protected GameObject[] meshes;

            public int MeshesCount
            {
                get { return meshes != null ? meshes.Length : 0; }
            }

            private int _selectedMesh = -1;
            public int SelectedMesh
            {
                get { return _selectedMesh; }
                set
                {
                    if (SelectedMesh != -1) {
                        meshes[_selectedMesh].SetActive(false);
                        meshes[_selectedMesh].transform.SetParent(parent.transform);
                    }

                    if (value >= MeshesCount) {
                        value = -1;
                    }
                    if (value < -1) {
                        value = MeshesCount - 1;
                    }
                    _selectedMesh = value;
                }
            }


            protected AbstractFXMesh(Transform anchor, FXType type) {
                this.anchor = anchor;
                this.fxType = type;

                parent = GameObject.Find("ModularMesh");
                if (parent == null) {
                    parent = new GameObject("ModularMesh");
                }

                GameObject[] meshes;
                IMeshLoader loader = new MeshLoader();
                loader.ParseMeshes(this, out meshes);

                this.meshes = new GameObject[meshes.Length];

                for (int i = 0; i < MeshesCount; i++) {
                    this.meshes[i] = GameObject.Instantiate(meshes[i]);
                    this.meshes[i].transform.SetParent(parent.transform);

                    this.meshes[i].SetActive(false);
                }

            }

            public abstract string GetFolderPath();

            public GameObject GetMesh() {
                return SelectedMesh == -1 ? null : meshes[SelectedMesh];
            }

            public void UpdateMesh() {
                if (SelectedMesh != -1) {
                    meshes[SelectedMesh].transform.SetParent(anchor);
                    meshes[SelectedMesh].transform.position = anchor.position;
                    meshes[SelectedMesh].transform.rotation = anchor.rotation;
                    meshes[SelectedMesh].SetActive(true);
                }
            }

            private void UnsetMesh() {
                if (SelectedMesh != -1) {
                    meshes[SelectedMesh].SetActive(false);
                    meshes[SelectedMesh].transform.SetParent(parent.transform);
                }
                SelectedMesh = -1;
            }


            public void MoveNext() {
                SelectedMesh++;
                UpdateMesh();
            }

            public void MovePrev() {
                SelectedMesh--;
                UpdateMesh();
            }

            public void Reset() {
                UnsetMesh();
            }

            public void Shuffle() {
                SelectedMesh = UnityEngine.Random.Range(-1, MeshesCount);
                UpdateMesh();
            }

            public void SetMesh(int mesh) {
                SelectedMesh = mesh;
                UpdateMesh();
            }
        }
    }
}
