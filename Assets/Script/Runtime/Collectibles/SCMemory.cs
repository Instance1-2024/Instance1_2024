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

        private void Collect(int amount)
        {
           //amount++;
           //_collectibleAmount = amount;
            
        }
        public void Interact()
        {
            Debug.Log("Collected");
            Destroy(this.gameObject);
        }
        
    }
}