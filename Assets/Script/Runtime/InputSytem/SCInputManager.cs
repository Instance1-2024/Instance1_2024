using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Script.Runtime.InputSystem {
    public class SCInputManager : MonoBehaviour {
        public static SCInputManager Instance { get; private set; }
        
        public bool IsKeyboard;
        public bool IsInputActive;
        
        public SInputEvent OnMoveEvent;
        public float MoveValue;
        
        public SInputEvent OnJumpEvent;
        
        public SInputEvent OnChangeColorEvent;
        
        public SInputEvent OnInteractEvent;
        
        public SInputEvent OnThrowEvent;
        public SInputEvent OnStartThrowEvent;
        
        public SInputEvent OnPauseEvent;

        public SInputEvent OnAimEvent;
        public Vector2 AimValue;
        
        private void Awake() {
            IsInputActive = true;
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(transform.root.gameObject);
            }
            else {
                DestroyImmediate(gameObject);
            }
        }

        /// <summary>
        /// When the player press any key.
        /// It will set IsKeyboard to true if the device is keyboard or mouse
        /// It will set IsKeyboard to false if the device is gamepad
        /// </summary>
        /// <param name="ctx">context of the input</param>
        public void OnAny(InputAction.CallbackContext ctx) {
            if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") {
                IsKeyboard = true;
            }
            else {
                IsKeyboard = false;
            }
        }

        /// <summary>
        /// When the player move, it will invoke the event
        /// </summary>
        /// <param name="ctx">context of the input</param>
        public void OnMove(InputAction.CallbackContext ctx) {
            if(!IsInputActive) return;
            MoveValue = ctx.ReadValue<float>();
            InvokeInputEvent(ctx, OnMoveEvent);
        }

        /// <summary>
        /// When the player Jump, it will invoke the event
        /// </summary>
        /// <param name="ctx">context of the input</param>
        public void OnJump(InputAction.CallbackContext ctx) {
            if(!IsInputActive) return;
            InvokeInputEvent(ctx, OnJumpEvent);
        }
        
        /// <summary>
        /// When the player Change Color, it will invoke the event
        /// </summary>
        /// <param name="ctx">context of the input</param>
        public void OnChangeColor(InputAction.CallbackContext ctx) {
            if(!IsInputActive) return;
            InvokeInputEvent(ctx, OnChangeColorEvent);
        }
        
        /// <summary>
        /// When the player interact, it will invoke the event
        /// </summary>
        /// <param name="ctx">context of the input</param>
        public void OnInteract(InputAction.CallbackContext ctx) {
            if(!IsInputActive) return;
            InvokeInputEvent(ctx, OnInteractEvent);
        }
        
        /// <summary>
        /// When the player throw, it will invoke the event
        /// </summary>
        /// <param name="ctx">context of the input</param>
        public void OnThrow(InputAction.CallbackContext ctx) {
            if(!IsInputActive) return;
            InvokeInputEvent(ctx, OnThrowEvent);
        }
        
        /// <summary>
        /// When the player Prepare to throw, it will invoke the event
        /// </summary>
        /// <param name="ctx">context of the input</param>
        public void OnStartThrow(InputAction.CallbackContext ctx) {
            if(!IsInputActive) return;
            InvokeInputEvent(ctx, OnStartThrowEvent);
        }
        
        /// <summary>
        /// When the player pause, it will invoke the event
        /// </summary>
        /// <param name="ctx">context of the input</param>
        public void OnPause(InputAction.CallbackContext ctx) {
            if(!IsInputActive) return;
            InvokeInputEvent(ctx, OnPauseEvent);
        }
        
        /// <summary>
        /// When the player aim with the gamepad, it will invoke the event
        /// </summary>
        /// <param name="ctx">context of the input</param>
        public void OnAim(InputAction.CallbackContext ctx) {
            if(!IsInputActive) return;
            AimValue = ctx.ReadValue<Vector2>();
            InvokeInputEvent(ctx, OnAimEvent);
        }

        /// <summary>
        ///  Invoke the input by the event
        /// </summary>
        /// <param name="ctx">The context of the input</param>
        /// <param name="inputEvent"> The event to invoke</param>
        void InvokeInputEvent(InputAction.CallbackContext ctx, SInputEvent inputEvent) {
            
            if (ctx.started) {
                inputEvent.Started?.Invoke();
            } else if (ctx.performed) {
                inputEvent.Performed?.Invoke();
            } else if (ctx.canceled) {
                inputEvent.Canceled?.Invoke();
            }
        }

        /// <summary>
        /// Event related to the input
        /// </summary>
        /// <param name="Started">When the input is started</param>
        /// <param name="Canceled">When the input is canceled</param>
        ///  <param name="Performed">When the input is performed</param>
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
