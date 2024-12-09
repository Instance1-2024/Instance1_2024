using System;
using Script.Runtime.Interact;
using UnityEngine;

namespace Script.Runtime.Pebble {
    public class SCPebble : MonoBehaviour, IInteractable  {
        [field: SerializeField]public Sprite Sprite { get; set; }
        [field: SerializeField]public bool CanBeHold { get; set; }
        private void Start() {
            CanBeHold = true;
        }

        public void Interact() {
        }

    }
}