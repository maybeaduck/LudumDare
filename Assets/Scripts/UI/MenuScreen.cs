using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

namespace Zlodey
{
    public class MenuScreen : Screen
    {
        public Button StartGameButton;
        public Button SettingsButton;
        public SettingsScreen SettingsWindow;
        public Button ExitButton;
        public SoundButton SoundButton;
        public HapticButton HapticButton;
        private void Start()
        {
            StartGameButton.onClick.AddListener(StartGame);
            SettingsButton.onClick.AddListener(Settings);
            ExitButton.onClick.AddListener(Exit);
        }

        public void Exit()
        {
            Application.Quit();
        }
        public void Settings()
        {
            SettingsWindow.gameObject.SetActive(!SettingsWindow.gameObject.activeSelf);
        }
        public void StartGame()
        {
            EcsWorld _world = Service<EcsWorld>.Get();
            EcsEntity _startEvent = _world.NewEntity();
            _startEvent.Get<ChangeGameStateEvent>().State = GameState.Play;
            Service<UI>.Get().GameScreen.Show();
            Service<UI>.Get().WaveScreen.gameObject.SetActive(true);
            Service<SceneData>.Get().Lobby.gameObject.SetActive(false);
            Hide();
        }
    }
}
