using System;
using Script.Runtime.Pebble;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Runtime.Player {
    public class SCPlayerHold : MonoBehaviour {
        public GameObject HoldItem;


        [SerializeField] private GameObject _holdSlot;
        Image _holdSlotImage;
        public Transform HoldPoint;
        [SerializeField] private Transform _meshTrans;

        private Rigidbody _holdItemBody;

        public bool CanHold => _meshTrans.rotation == Quaternion.Euler(0f, -180f, 0f) || _meshTrans.rotation == Quaternion.Euler(0f, 0f, 0f);

        public bool IsHolding => HoldItem != null;
        
        private void Start() {
            _holdSlot.SetActive(false);
            _holdSlotImage = _holdSlot.transform.GetChild(0).GetComponent<Image>();
        }

        
        /// <summary>
        /// Take the item to the inventory
        /// </summary>
        /// <param name="item"> The item to take</param>
        public void Hold( GameObject item) {
            if (!CanHold) return;
            
            HoldItem = item.transform.parent.gameObject;
            HoldItem.GetComponent<Rigidbody>().isKinematic = true;
            HoldItem.transform.SetParent(HoldPoint);
            if(HoldItem.TryGetComponent(out IThrowable throwable)) {
                throwable.RemoveColllisions();
            }   
            HoldItem.transform.localPosition = Vector3.zero;
            HoldItem.GetComponentInChildren<Collider>().enabled = false;
            HoldItem.SetActive(false);
        }
        
        /// <summary>
        /// Drop the item from the inventory in front of the player
        /// </summary>
        public void Drop() {
            if (!CanHold) return;
            
            HoldItem.SetActive(true);
            if(HoldItem.TryGetComponent(out IThrowable throwable)) {
                throwable.GiveCollisions();
            }
            HoldItem.GetComponentInChildren<Collider>().enabled = true;
            HoldItem.GetComponent<Rigidbody>().isKinematic = false;
            HoldItem.transform.SetParent(null);

            RemoveHoldImage();
        }

        /// <summary>
        /// Set the sprite to the hold slot
        /// </summary>
        /// <param name="sprite">Sprite of the item</param>
        public void SetHoldImage(Sprite sprite) {
            _holdSlot.SetActive(true);
            _holdSlotImage.sprite = sprite;
        }
        
        
        /// <summary>
        /// Remove the sprite from the hold slot
        /// </summary>
        public void RemoveHoldImage() {
            _holdSlot.SetActive(false);
            _holdSlotImage.sprite = null;
            HoldItem = null;
        }
    }
}