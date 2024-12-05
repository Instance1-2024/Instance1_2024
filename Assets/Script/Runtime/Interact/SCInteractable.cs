using UnityEngine;

namespace Script.Runtime.Interact {
    public class SCInteractable : MonoBehaviour, IInteractable {
        public void Interact() {
            Debug.Log(gameObject.name);
        }
    }
}

