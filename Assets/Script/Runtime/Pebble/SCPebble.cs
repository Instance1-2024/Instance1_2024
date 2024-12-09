using System;
using Script.Runtime.Interact;

namespace Script.Runtime.Pebble {
    public class SCPebble : SCInteractable {
        private void Start() {
            CanBeHold = true;
        }
    }
}