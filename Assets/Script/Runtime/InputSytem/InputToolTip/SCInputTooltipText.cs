using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Script.Runtime.InputSystem.InputTooltip {
    public class SCInputTooltipText : MonoBehaviour {
        [Tooltip("Reference to action that is to be displayed from the UI.")]
        [SerializeField] InputActionReference _action;

        [SerializeField] string _bindingID;

        public string _bindingText;
        
        /// <summary>
        /// ID (in string form) of the binding that is to be rebound on the action.
        /// </summary>
        /// <seealso cref="InputBinding.id"/>
        public string BindingId {
            get => _bindingID;
            set {
                _bindingID = value;
                UpdateBindingValue();
            }
        }
        
        /// <summary>
        /// Text component that receives the display string of the binding./>.
        /// </summary>
        public string BindingText {
            get => _bindingText;
            set {
                _bindingText = value;
                UpdateBindingValue();
            }
        }
        
        /// <summary>
        /// Trigger a refresh of the currently displayed binding.
        /// </summary>
        public void UpdateBindingValue() {
            string displayString = string.Empty;
            string deviceLayoutName = default;
            string controlPath = default;

            //Get display string from action.
            InputAction action = _action?.action;
            if (action != null) {
                int bindingIndex = action.bindings.IndexOf(u => u.id.ToString() == _bindingID);
                if (bindingIndex != -1) {
                    displayString = action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath);
                }
            }
            
            if (_bindingText != null){
                _bindingText = displayString;
            }
        }
        
        #if UNITY_EDITOR
                protected void OnValidate() {
                    UpdateBindingValue();
                }

        #endif
    }
}