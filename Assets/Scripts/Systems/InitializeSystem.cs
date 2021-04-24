using System.Collections.Generic;
using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Zlodey
{
    public class InitializeSystem : Injects, IEcsInitSystem
    {
        private SceneData _sceneData;
        private RuntimeData _runtimeData;
        private EcsWorld _world;
        private UI _ui;
        public void Init()
        {
            // AudioSource Instantiate
            var spawnedAudioSource = GameObject.Find("AudioSource")?.GetComponent<AudioSource>();
            if (spawnedAudioSource == null)
            {
                var audioSource = Object.Instantiate(Config.AudioSourcePrefab);
                audioSource.name = "AudioSource";
                Object.DontDestroyOnLoad(audioSource);
                Runtime.AudioSource = audioSource;
            }
            else
            {
                Runtime.AudioSource = spawnedAudioSource;
            }
           
            if (Progress.CurentSound == 0)
            {
                _ui.MenuScreen.SoundButton.SwitchImage();    
            }
            if (Progress.CurentHaptic == 0)
            {
                _ui.MenuScreen.HapticButton.SwitchImage();    
            }
            if(Progress.CurentHaptic == 0){
                Runtime.HapticOn = false;
            }
            else if(Progress.CurentHaptic == 1){
                Runtime.HapticOn = true;
            }
            
            if(Progress.CurentSound == 0){
                Runtime.SoundOn = false;
            }
            else if(Progress.CurentSound == 1){
                Runtime.SoundOn = true;
            }
        }
    }
}