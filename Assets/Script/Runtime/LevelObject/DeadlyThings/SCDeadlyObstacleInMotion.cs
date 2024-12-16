using System;
using Script.Runtime.Player;
using UnityEngine;

namespace Script.Runtime.LevelObject.DeadlyThings {
    public class SCDeadlyObstacleInMotion : MonoBehaviour {
        private Rigidbody _body;
        bool _isInMotion;
        
        private void Start() {
            _body = GetComponent<Rigidbody>();
        }

        private void Update() {
            _isInMotion = _body.linearVelocity != Vector3.zero;
        }

        /// <summary>
        /// Death of player on collision with Spike
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter(Collision other) {
            if(!_isInMotion) return;
            if (other.gameObject.CompareTag("Player")) {
                Debug.Log("Death");
                other.gameObject.GetComponent<SCPlayerRespawnAtCheckpoint>().OnRespawn();

            }

            if (other.gameObject.CompareTag("Pebble")) {
                Destroy(other.gameObject);
            }
        }
    }
}

