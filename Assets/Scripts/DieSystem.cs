using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Zlodey
{
    internal class DieSystem : Injects,IEcsRunSystem
    {
        private EcsFilter<PersonData, CharacterStatsComponent, DieEvent> _die;
        private EcsFilter<ShootingSceletonData, DieFlag> _dieShotingSceleton;
        private EcsFilter<PersonData, DieFlag> _owerDie;
        private EcsFilter<RushersData, DieFlag> _rushersDie;
        private EcsFilter<PersonData, DieFlag> _diePerson;
        public void Run()
        {
            foreach (var item in _diePerson)
            {
                ref var personData = ref _diePerson.Get1(item);
                ref var time = ref _diePerson.Get2(item).DieTime;
                time += Time.deltaTime;
                if (time >= _staticData.DeleteTrupsTime)
                {
                    Object.Destroy(personData.Actor.gameObject);
                    _diePerson.GetEntity(item).Destroy();
                }
            }
            foreach (var item in _die)
            {
                ref var personData = ref _die.Get1(item);
                personData.Actor.Animator.SetBool("Die",true);
                personData.Actor.Animator.SetLayerWeight(personData.Actor.Animator.GetLayerIndex("hand"), 0f);
                personData.Actor.GetComponent<Collider>().enabled = false;
                personData.Rigidbody.isKinematic = true;
                personData.Actor.HealthBar.SetActive(false);
                if (Random.Range(0f, 1f) <= personData.Actor.ChanceDropItem)
                {
                    List<drop> dropItemRoll = new List<drop>();
                    foreach (var i in _staticData.DropItems)
                    {
                        if (Random.Range(0, 1) < i.chance)
                        {
                            dropItemRoll.Add(i);
                        }    
                    }

                    var toDrop = dropItemRoll[Random.Range(0, dropItemRoll.Count - 1)].DropItem;
                    GameObject.Instantiate(toDrop, personData.Actor.transform.position, Quaternion.identity);

                }
                _die.GetEntity(item).Get<DieFlag>();
                _die.GetEntity(item).Del<DieEvent>();
                
            }

            foreach (var item in _rushersDie)
            {
                _rushersDie.Get1(item).botFilter.enabled = false;
                _rushersDie.Get1(item).AttackZone.enabled = false;
                _rushersDie.Get1(item).meshAgent.Stop(true);
            }

            foreach (var item in _dieShotingSceleton)
            {
                _dieShotingSceleton.Get1(item).meshAgent.Stop(true);
            }
        }
    }
}