using UnityEngine;

namespace Script.Runtime.Interact {
    public interface IInteractable {

        public void Interact();
        public Sprite Sprite { get; set; }
        
        

    }
}