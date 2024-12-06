using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Runtime.Player {
    public class SCPlayerHold : MonoBehaviour {
        public GameObject HoldItem;


        [SerializeField] private GameObject _holdSlot;
        Image _holdSlotImage;
        
        [SerializeField] private Transform _holdPoint;
        [SerializeField] private Transform _meshTrans;
        
        [SerializeField]float _throwSpeed = 0.25f;

        private Rigidbody _holdItemBody;

        public bool CanHold => _meshTrans.rotation == Quaternion.Euler(0f, -180f, 0f) || _meshTrans.rotation == Quaternion.Euler(0f, 0f, 0f);


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
            HoldItem.transform.SetParent(_holdPoint);
            HoldItem.GetComponentInChildren<Collider>().enabled = false;
            HoldItem.SetActive(false);
        }
        
        /// <summary>
        /// Drop the item from the inventory in front of the player
        /// </summary>
        public void Drop() {
            if (!CanHold) return;
            
            HoldItem.SetActive(true);
            HoldItem.GetComponentInChildren<Collider>().enabled = true;
            HoldItem.GetComponent<Rigidbody>().isKinematic = false;
            HoldItem.transform.SetParent(null);
            HoldItem = null;

            RemoveHoldImage();
        }

        /// <summary>
        /// Throw the item to the throw position
        /// </summary>
        /// <param name="throwPosition"> The position to throw</param>
        public void Throw(Vector3 throwPosition) {
            HoldItem.SetActive(true);
            HoldItem.transform.SetParent(null);
            
            Vector3 direction = (throwPosition - HoldItem.transform.position).normalized;
            
            _holdItemBody = HoldItem.GetComponent<Rigidbody>();
            
            _holdItemBody.isKinematic = false;
            _holdItemBody.mass = 0.1f;
            
            _holdItemBody.AddForce(direction * _throwSpeed, ForceMode.Impulse);
            
            Invoke(nameof(ActiveCollider), 0.15f);
        }

        /// <summary>
        /// Active the collider and the rigidbody then remove to the inventory
        /// </summary>
        private void ActiveCollider() {
            HoldItem.GetComponentInChildren<Collider>().enabled = true;
            _holdItemBody.mass = 1f;
            HoldItem = null;

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
        }
    }
}