using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class PersonLookAtMouseSystem : Injects, IEcsRunSystem
    {
        private EcsWorld _world;
        private RuntimeData _runtime;
        private SceneData _scene;
        private StaticData _static;
        private EcsFilter<PersonData,PlayerData>.Exclude<StandFlag,DieFlag> _activePersons;
        public void Run()
        {
            if (_runtimeData.GameState != GameState.Play)
            {
                return;
            }
            foreach (var item in _activePersons)
            {
                ref var entity = ref _activePersons.GetEntity(item);
                ref var person = ref _activePersons.Get1(item).Actor;

                if (entity.Has<Dash>())
                {
                    return;
                }
                
                var position = person.transform.position;
                Plane plane = new Plane(Vector3.up,position + Vector3.up * _static.Visota);
                Ray ray = _scene.Camera.ScreenPointToRay(Input.mousePosition);
               
                if (plane.Raycast(ray, out var hit))
                {
                    person.transform.eulerAngles = new Vector3(0,person.transform.eulerAngles.y,0);
                    Vector3 target = ray.GetPoint(hit);
                    Quaternion rotation = Quaternion.LookRotation(target - position);
                    
                    person.transform.rotation = Quaternion.Slerp(person.transform.rotation , rotation ,
                        _static.speedRotation * Time.deltaTime);
                    Debug.DrawRay(target,Vector3.up*10,Color.red);
                    person.transform.eulerAngles = new Vector3(0,person.transform.eulerAngles.y,0);
                }
            }
        }
    }
}