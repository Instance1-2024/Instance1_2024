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
        public bool CanSpawn;

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
            //The timer that start at 0 by default, which makes spawn instantly the first object
            _time -= Time.deltaTime ;

            if (_time <= 0) {
                //Reset the timer
                _time = _timeBetweenSpawn ;
    
                
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

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// This spawn instantiate an object when CanSpawn == true which is the case at the start and when the previous object is destroyed
        /// </summary>
        void Spawn2() {
            if (CanSpawn) {
                //Prevents the spawn of an object as long as the previous one is not destroyed and therefore returns in OnDestroy : CanSpawn = true
                CanSpawn = false;

                //Instantiates the prefab specified in the editor
                

                //Adds a force to the spawn of the object
                

                //Adds the script of life span to the instantiated object
                
                
                //Adds a reference to this script for the instantiated object
                
            }

        }
    }
}