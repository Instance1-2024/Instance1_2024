using UnityEngine;

namespace Script.Runtime.Player {
    public class SCPlayerHold : MonoBehaviour {
        public GameObject HoldItem;
        
        [SerializeField] private Transform _holdPoint;
        
        public void Hold( GameObject item) {
            HoldItem = item.transform.parent.gameObject;
            HoldItem.GetComponent<Rigidbody>().isKinematic = true;
            HoldItem.transform.SetParent(_holdPoint);
            item.GetComponent<Collider>().enabled = false;
        }
    }
}