using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using SkeletonEditor.FX;
using SkeletonEditor.Mesh;
using UnityEngine.UI;

namespace SkeletonEditor
{

    public class MeshManager : MonoBehaviour
    {
        private string characterRace;

        private Dictionary<MeshType, AbstractMesh> characterMeshes;
        private Dictionary<FXType, AbstractFXMesh> fxMeshes;

        private Texture2D armorTexture;
        
        public List<AbstractMesh> SelectedMeshes {
            get; private set;
        }

        public static MeshManager Instance { get; private set; }

        void Awake() {
            if (Instance != null) {
                Destroy(this.gameObject);
            }
            Instance = this;
        }

        // Use this for initialization
        void Start() {
            // this will allow in the future update more character race

            characterMeshes = new Dictionary<MeshType, AbstractMesh>
            {
                {MeshType.Jaw, new Jaw(GameObject.Find("Bip01 Ponytail1").transform) },
                {MeshType.Helm, new Helm(GameObject.Find("Bip01 Head").transform) },
                {MeshType.Torso, new Torso(GameObject.Find("Bip01 Spine2").transform) },
                {MeshType.TorsoAdd, new TorsoAdd(GameObject.Find("Bip01 Spine2").transform) },
                {MeshType.LegLeft, new Leg(GameObject.Find("Bip01 L Calf").transform, MeshType.LegLeft) },
                {MeshType.LegRight, new Leg(GameObject.Find("Bip01 R Calf").transform, MeshType.LegRight) },
                {MeshType.ShoulderLeft, new Shoulder(GameObject.Find("Bip01 L UpperArm").transform, MeshType.ShoulderLeft) },
                {MeshType.ShoulderRight, new Shoulder(GameObject.Find("Bip01 R UpperArm").transform, MeshType.ShoulderRight) },
                {MeshType.ArmLeft, new Arm(GameObject.Find("Bip01 L Forearm").transform, MeshType.ArmLeft) },
                {MeshType.ArmRight, new Arm(GameObject.Find("Bip01 R Forearm").transform, MeshType.ArmRight) },
                {MeshType.Belt, new Belt(GameObject.Find("Bip01 Pelvis").transform) },
                {MeshType.BeltAdd, new BeltAdd(GameObject.Find("Bip01 Pelvis").transform) },
                {MeshType.HandLeft, new Hand(GameObject.Find("Bip01 L Weapon").transform, MeshType.HandLeft) },
                {MeshType.HandRight, new Hand(GameObject.Find("Bip01 R Weapon").transform, MeshType.HandRight) },
            };

            fxMeshes = new Dictionary<FXType, AbstractFXMesh>
            {
                {FXType.Torso, new TorsoFX(GameObject.Find("Bip01 Spine2").transform) },
                {FXType.Eye, new EyeFX(GameObject.Find("Bip01 Head").transform) },
            };

            SelectedMeshes = new List<AbstractMesh>();
        }

        public void BuildTexture() {
            //todo
        }

        public Texture2D GetMergedTexture() {
            return armorTexture;
        }

        #region Mesh Actions
        public void OnNextMesh(MeshType[] types) {
            for (int i = 0; i < types.Length; i++) {
                characterMeshes[types[i]].MoveNext();
            }
            OnChangeMesh();
        }

        public void OnPrevMesh(MeshType[] types) {
            for (int i = 0; i < types.Length; i++) {
                characterMeshes[types[i]].MovePrev();
            }
            OnChangeMesh();
        }

        public void OnClearMesh(MeshType[] types) {
            for (int i = 0; i < types.Length; i++) {
                characterMeshes[types[i]].Reset();
            }
            OnChangeMesh();
        }

        public void OnRandom(MeshType[] types, MeshType[][] sameTypes = null) {
            //Remove same meshes from random list
            if (sameTypes != null && sameTypes.Length > 0)
            {
                List<MeshType> typesList = new List<MeshType>(types);
                foreach (MeshType[] typeGroup in sameTypes)
                {
                    for (int i = 1; i < typeGroup.Length; i++)
                    {
                        typesList.Remove(typeGroup[i]);
                    }
                }
                types = typesList.ToArray();
            }

            //Shuffle available types
            for (int i = 0; i < types.Length; i++) {
                characterMeshes[types[i]].Shuffle();
            }

            if (sameTypes != null)
            {
                foreach (MeshType[] typeGroup in sameTypes)
                {
                    for (int i = 1; i < typeGroup.Length; i++)
                    {
                        characterMeshes[typeGroup[i]].SetMesh(characterMeshes[typeGroup[0]].SelectedMesh);
                    }
                }
            }
            OnChangeMesh();
        }
        #endregion

        #region FX Actions
        public void OnNextFX(FXType[] types) {
            for (int i = 0; i < types.Length; i++) {
                fxMeshes[types[i]].MoveNext();
            }
        }

        public void OnPrevFX(FXType[] types) {
            for (int i = 0; i < types.Length; i++) {
                fxMeshes[types[i]].MovePrev();
            }
        }

        public void OnClearFX(FXType[] types) {
            for (int i = 0; i < types.Length; i++) {
                fxMeshes[types[i]].Reset();
            }
        }

        public void OnRandomFX(FXType[] types) {
            for (int i = 0; i < types.Length; i++) {
                fxMeshes[types[i]].Shuffle();
            }
        }
        #endregion

        private void OnChangeMesh() {
            BuildTexture();
        }
    }
}
