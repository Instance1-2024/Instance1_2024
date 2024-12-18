using UnityEngine;
using System.Collections;

namespace Script.Runtime.Platform {
    public class SCColorShowOnCollision : MonoBehaviour {
        [SerializeField] private Animation _animation;

        private void Start() {
            _animation = GetComponentInChildren<Animation>();
        }

        /// <summary>
        /// Start the platform apparition animation on collision after stop the previous one
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter(Collision other) {
            Debug.Log("Collision" + other.gameObject.name);
            if (other.gameObject.CompareTag("Pebble")) {
                _animation.Stop();
                _animation.Play(_animation.clip.name);
            }
        }
    }
}
