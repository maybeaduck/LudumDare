using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class PersonLookAtMouseSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private RuntimeData _runtime;
        private SceneData _scene;
        private StaticData _static;
        private EcsFilter<PersonData,PlayerData>.Exclude<StandFlag,DieFlag> _activePersons;
        public void Run()
        {
            
            foreach (var item in _activePersons)
            {
                
                ref var person = ref _activePersons.Get1(item).Actor;
                var position = person.transform.position;
                Plane plane = new Plane(Vector3.up,position + Vector3.up * 4);
                Ray ray = _scene.Camera.ScreenPointToRay(Input.mousePosition);
               
                if (plane.Raycast(ray, out var hit))
                {
                    Vector3 target = ray.GetPoint(hit);
                    Quaternion rotation = Quaternion.LookRotation(target - position);
                    
                    person.transform.rotation = Quaternion.Slerp(person.transform.rotation , rotation ,
                        _static.speedRotation * Time.deltaTime);
                    Debug.DrawRay(target,Vector3.up*10,Color.red);
                    
                }
            }
        }
    }
}