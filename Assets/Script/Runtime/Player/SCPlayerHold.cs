using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Runtime.Player {
    public class SCPlayerHold : MonoBehaviour {
        public GameObject HoldItem;


        [SerializeField] private GameObject _holdSlot;
        Image _holdSlotImage;
        
        [SerializeField] private Transform _holdPoint;
        [SerializeField] private Transform _meshTrans;

        public bool CanHold => _meshTrans.rotation == Quaternion.Euler(0f, -180f, 0f) || _meshTrans.rotation == Quaternion.Euler(0f, 0f, 0f);


        private void Start() {
            _holdSlot.SetActive(false);
            _holdSlotImage = _holdSlot.transform.GetChild(0).GetComponent<Image>();
        }

        public void Hold( GameObject item) {
            if (!CanHold) return;
            
            HoldItem = item.transform.parent.gameObject;
            HoldItem.GetComponent<Rigidbody>().isKinematic = true;
            HoldItem.transform.SetParent(_holdPoint);
            HoldItem.GetComponentInChildren<Collider>().enabled = false;
            HoldItem.SetActive(false);
        }
        
        public void Drop() {
            if (!CanHold) return;
            
            HoldItem.SetActive(true);
            HoldItem.GetComponentInChildren<Collider>().enabled = true;
            HoldItem.GetComponent<Rigidbody>().isKinematic = false;
            HoldItem.transform.SetParent(null);
            HoldItem = null;

            RemoveHoldImage();
        }


        public void SetHoldImage(Sprite sprite) {
            _holdSlot.SetActive(true);
            _holdSlotImage.sprite = sprite;
        }
        
        public void RemoveHoldImage() {
            _holdSlot.SetActive(false);
            _holdSlotImage.sprite = null;
        }
    }
}