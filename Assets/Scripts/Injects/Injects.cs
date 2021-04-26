using Leopotam.Ecs;
using LeopotamGroup.Globals;

namespace Zlodey
{
    public class Injects
    {
        public StaticData _staticData = Service<StaticData>.Get();
        public RuntimeData _runtimeData= Service<RuntimeData>.Get();
        public SceneData _sceneData= Service<SceneData>.Get();
        public EcsWorld _world= Service<EcsWorld>.Get();
        public UI _ui= Service<UI>.Get();
    }
}