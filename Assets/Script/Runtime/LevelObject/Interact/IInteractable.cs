using UnityEngine;

namespace Script.Runtime.LevelObject.Interact {
    public interface IInteractable {

        public void Interact();
        public Sprite Sprite { get; set; }
        
        public bool CanBeHold { get; set; }
        
        

    }
}