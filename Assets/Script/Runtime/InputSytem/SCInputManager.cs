using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Script.Runtime.InputSystem {
    public class SCInputManager : MonoBehaviour {
        public SCInputManager Instance { get; private set; }
        
        public SInputEvent OnMoveEvent;
        public float MoveValue;
        
        public SInputEvent OnJumpEvent;
        
        public SInputEvent OnChangeColorEvent;
        
        public SInputEvent OnInteractEvent;
        
        public SInputEvent OnDropEvent;
        public SInputEvent OnPauseEvent;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(transform.root.gameObject);
            }
            else {
                DestroyImmediate(gameObject);
            }
        }

        public void OnMove(InputAction.CallbackContext ctx) {
            MoveValue = ctx.ReadValue<float>();
            InvokeInputEvent(ctx, OnMoveEvent);
        }

        public void OnJump(InputAction.CallbackContext ctx) {
            InvokeInputEvent(ctx, OnJumpEvent);
        }
        
        public void OnChangeColor(InputAction.CallbackContext ctx) {
            InvokeInputEvent(ctx, OnChangeColorEvent);
        }

        public void OnInteract(InputAction.CallbackContext ctx) {
            InvokeInputEvent(ctx, OnInteractEvent);
        }

        public void OnDrop(InputAction.CallbackContext ctx) {
            InvokeInputEvent(ctx, OnDropEvent);
        }
        
        public void OnPause(InputAction.CallbackContext ctx) {
            InvokeInputEvent(ctx, OnPauseEvent);
        }

        void InvokeInputEvent(InputAction.CallbackContext ctx, SInputEvent inputEvent) {
            if (ctx.started) {
                inputEvent.Started?.Invoke();
            } else if (ctx.performed) {
                inputEvent.Performed?.Invoke();
            } else if (ctx.canceled) {
                inputEvent.Canceled?.Invoke();
            }
        }

        [System.Serializable]
        public struct SInputEvent {
            public UnityEvent Started;
            public UnityEvent Performed;
            public UnityEvent Canceled;
            
            public SInputEvent(UnityEvent started, UnityEvent canceled, UnityEvent performed) {
                Started = started ?? new UnityEvent();
                Canceled = canceled ?? new UnityEvent();
                Performed = performed ?? new UnityEvent();
            }
        }
    }
}
