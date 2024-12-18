using Script.Runtime.ColorManagement;
using Script.Runtime.LevelObject.Interact;
using UnityEngine;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.LevelObject.Pebble {
    public class SCPebble : MonoBehaviour, IInteractable, IThrowable {
        [field: SerializeField]public Sprite Sprite { get; set; }
        [field: SerializeField]public bool CanBeHold { get; set; }
        
        
        [field: SerializeField]public LayerMask ThrowBlack { get;set;  }
        [field: SerializeField]public LayerMask ThrowGray { get;set;  }
        [field: SerializeField]public LayerMask ThrowWhite { get; set; }

        public bool IsColliding ;

        private SCChangeColor _changeColor;
        
        private void Start() {
            CanBeHold = true;
            _changeColor = GetComponent<SCChangeColor>();
        }

        public void Interact() {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }

        private void OnCollisionEnter(Collision other) {
            if (!other.gameObject.CompareTag("Player")) {
                IsColliding = true;
            }
            
        }
        
        private void OnCollisionExit(Collision other) {
            IsColliding = false;
        }
        
        /// <summary>
        /// Remove the collision with the Player
        /// </summary>
        public void RemoveCollisions() {
            switch (_changeColor.GetColor()) {
                case EColor.Black:
                    gameObject.layer = (int)Mathf.Log(ThrowBlack.value, 2);
                    foreach (Transform child in transform) {
                        child.gameObject.layer = (int)Mathf.Log(ThrowBlack.value, 2);
                    }
                    
                    break;
                case EColor.White:
                    gameObject.layer = (int)Mathf.Log(ThrowWhite.value, 2);
                    foreach (Transform child in transform) {
                        child.gameObject.layer = (int)Mathf.Log(ThrowWhite.value, 2);
                    }
                    break;
                case EColor.Gray:
                    gameObject.layer = (int)Mathf.Log(ThrowGray.value, 2);
                    foreach (Transform child in transform) {
                        child.gameObject.layer = (int)Mathf.Log(ThrowGray.value, 2);
                    }
                    break;
            }
        }
        
        /// <summary>
        /// Add the collision to the Player
        /// </summary>
        public void GiveCollisions() {
            _changeColor.ChangeColor(_changeColor.GetColor());
        }
    }
}