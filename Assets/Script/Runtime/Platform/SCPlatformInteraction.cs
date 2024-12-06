using UnityEngine;
using System.Collections;

namespace Script.Runtime.Platform {
    [RequireComponent(typeof(Animation))]
    public class SCPlatformInteraction : MonoBehaviour {
        [SerializeField] private Animation _animation;

        private void Start() {
            _animation = GetComponent<Animation>();
        }

        /// <summary>
        /// Start the platform apparition animation on collision after stop the previous one
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Pebble")) {

                _animation.Stop();


                _animation.Play(_animation.clip.name);
                Debug.Log("hit");
            }
        }
    }
}
