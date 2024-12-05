using UnityEngine;

namespace Script.Runtime.Player {
    public class SCPlayerHold : MonoBehaviour {
        public GameObject HoldItem;
        
        [SerializeField] private Transform _holdPoint;
        [SerializeField] private Transform _meshTrans;

        public bool CanHold => _meshTrans.rotation == Quaternion.Euler(0f, -180f, 0f) || _meshTrans.rotation == Quaternion.Euler(0f, 0f, 0f);
        
        
        public void Hold( GameObject item) {
            if (!CanHold) return;
            
            HoldItem = item.transform.parent.gameObject;
            HoldItem.GetComponent<Rigidbody>().isKinematic = true;
            HoldItem.transform.SetParent(_holdPoint);
            HoldItem.GetComponentInChildren<Collider>().enabled = false;
        }
        
        public void Drop() {
            if (!CanHold) return;
            
            HoldItem.GetComponentInChildren<Collider>().enabled = true;
            HoldItem.GetComponent<Rigidbody>().isKinematic = false;
            HoldItem.transform.SetParent(null);
            HoldItem = null;
        }
    }
}