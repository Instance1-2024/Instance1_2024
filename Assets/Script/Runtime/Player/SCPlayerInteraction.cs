using System.Linq;
using Script.Runtime.InputSystem;
using Script.Runtime.Interact;
using UnityEngine;

namespace Script.Runtime.Player {
    public class SCPlayerInteraction : MonoBehaviour {
        
        [SerializeField] Transform _interactPoint;
        SCInputManager _inputManager => SCInputManager.Instance;
        [SerializeField] LayerMask _interactMask;

        private void Start() {
            _inputManager.OnInteractEvent.Started.AddListener(InteractStart);
        }


        void InteractStart() {
            Vector3 size = new(1f, 1.8f, 1f);
            Collider[] hits = Physics.OverlapBox(_interactPoint.position + Vector3.right * 0.5f, size, _interactPoint.rotation);
            
            foreach (Collider hit in hits.ToList().Where(hit => hit.transform.IsChildOf(transform) || hit.transform == transform)) {
                hits = hits.Where(h => h != hit).ToArray();
            }
            
            foreach (Collider hit in hits) {
                if (hit == null || hit.transform.parent == null) continue;
                if (hit.transform.parent.TryGetComponent<IInteractable>(out var interactable)) {
                    if (hit.gameObject.layer != gameObject.layer && hit.gameObject.layer != _interactMask) continue;
                    interactable.Interact();
                    return;
                }
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_interactPoint.position + Vector3.right *0.1f, new Vector3(1f, 1.8f, 1f));
        }
    }
}