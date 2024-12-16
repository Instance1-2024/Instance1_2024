using Script.Runtime.LevelObject.Interact;
using Script.Runtime.Prophecy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Script.Runtime.LevelObject.Collectibles
{
    public class SCMemory : MonoBehaviour, IInteractable
    {
        public Sprite Sprite { get; set; }

        public bool CanBeHold { get; set; }

        [Tooltip("Choose Between 1 and 7"), Range(1, 7), SerializeField]
        private int _memoryID;
        /// <summary>
        /// Interaction between player and memory, destroy GO when Interact button is pressed
        /// </summary>
        public void Interact()
        {
            Debug.Log("Collected"); 
            SCProphecyUIManager.Instance.GetMemoryEvent.Invoke(_memoryID,false);
            Destroy(this.gameObject);
        }
        
    }
}