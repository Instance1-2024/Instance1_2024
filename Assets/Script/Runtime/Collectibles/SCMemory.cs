using Script.Runtime.Interact;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Script.Runtime.Collectibles
{
    public class SCMemory : MonoBehaviour, IInteractable
    {
        public Sprite Sprite { get; set; }

        public bool CanBeHold { get; set; }


        /// <summary>
        /// Interaction between player and memory, destroy GO when Interact button is pressed
        /// </summary>
        public void Interact()
        {
            Debug.Log("Collected");
            Destroy(this.gameObject);
        }
        
    }
}