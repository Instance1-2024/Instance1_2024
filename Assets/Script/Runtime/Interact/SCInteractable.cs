using UnityEngine;

namespace Script.Runtime.Interact {
    public class SCInteractable : MonoBehaviour, IInteractable {
        [field:SerializeField] public Sprite Sprite { get; set; }
        [field:SerializeField] public bool CanBeHold { get; set; }

        public void Interact() {
            Debug.Log(gameObject.name);
        }

    }
}

