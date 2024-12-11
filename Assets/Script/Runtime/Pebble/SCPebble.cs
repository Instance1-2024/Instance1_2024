using Script.Runtime.ColorManagement;
using Script.Runtime.Interact;
using UnityEngine;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.Pebble {
    public class SCPebble : MonoBehaviour, IInteractable, IThrowable {
        [field: SerializeField]public Sprite Sprite { get; set; }
        [field: SerializeField]public bool CanBeHold { get; set; }
        
        
        [field: SerializeField]public LayerMask ThrowBlack { get;set;  }
        [field: SerializeField]public LayerMask ThrowGray { get;set;  }
        [field: SerializeField]public LayerMask ThrowWhite { get; set; }

        public bool IsColliding ;

        private SCColorChange _colorChange;
        
        private void Start() {
            CanBeHold = true;
            _colorChange = GetComponent<SCColorChange>();
        }

        public void Interact() {
        }

        private void OnCollisionEnter(Collision other) {
            if (!other.gameObject.CompareTag("Player")) {
                IsColliding = true;
            }
            
        }
        
        private void OnCollisionExit(Collision other) {
            IsColliding = false;
        }
        
        public void Take() {
            switch (_colorChange.GetColor()) {
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
        
        public void Reset() {
            _colorChange.ChangeColor(_colorChange.GetColor());
        }
    }
}