using Unity.VisualScripting;
using UnityEngine;

namespace Script.Runtime.LevelObject {
    public class SCTriggeredSpawner {
        private SCSpawner _spawner;
        
        
        
        void OnTriggerEnter() {
            _spawner.CanSpawn = true;
        }
    }
}