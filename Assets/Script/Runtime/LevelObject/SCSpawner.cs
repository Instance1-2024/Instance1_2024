using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Runtime.LevelObject {
    public class SCSpawner : MonoBehaviour {
        [SerializeField] GameObject _prefabToSpawn;
        [SerializeField] Transform _spawnPos;
        [SerializeField] float _timeBetweenSpawn;
        private float _time;
        [SerializeField] float _spawnForce ;
        [SerializeField] Vector3 _direction ;
        [SerializeField] bool _despawnObjectOnDestroy;
        public bool CanSpawn = true;

        void Update() {
            
            //Chooses the way to spawn an object
            if (CanSpawn) {
                Spawn(_despawnObjectOnDestroy);
            }

        }
        
        /// <summary>
        /// This spawn instantiate an object and destroy it every TimeBS
        /// </summary>
        void Spawn(bool despawnObjectOnDestroy) {
            if (despawnObjectOnDestroy && CanSpawn) {
                
                CanSpawn = false;
                InstantiatePrefab(true);
                
            }
            else {
                
                _time -= Time.deltaTime ;

                if (_time <= 0) {
                    _time = _timeBetweenSpawn ;
    
                    InstantiatePrefab(false);
                }
            }
        }

        void InstantiatePrefab(bool canDespawn) {
            GameObject prefab = Instantiate ( _prefabToSpawn ,  _spawnPos.position , Quaternion.identity );
            prefab.GetComponent<Rigidbody>().AddForce(_direction.normalized * _spawnForce);
            SCLifeObject lifeComp = prefab.AddComponent<SCLifeObject>();
            lifeComp.SpawnerScript = this;
            if (canDespawn) {
                lifeComp.LifeSpanObject = _timeBetweenSpawn;
                lifeComp.canDespawn = true;
            }

            
        }
    }
}