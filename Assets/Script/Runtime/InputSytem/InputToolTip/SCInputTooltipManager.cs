using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Runtime.InputSystem.InputTooltip {
    public class SCInputTooltipManager : MonoBehaviour {
        SCInputManager _inputManager => SCInputManager.Instance;
        public List<SCInputTooltipText> KeyboardTooltips = new();
        public List<SCInputTooltipText> GamepadTooltips = new();

        private void Start() {
            foreach (SCInputTooltipText tooltip in KeyboardTooltips) {
                tooltip.enabled = true;
            }
            foreach (SCInputTooltipText tooltip in GamepadTooltips) {
                tooltip.enabled = false;
            }
        }
        
        private void Update() {
            foreach (SCInputTooltipText tooltip in KeyboardTooltips){
                tooltip.enabled = _inputManager.IsKeyboard;
            }
            foreach (SCInputTooltipText tooltip in GamepadTooltips){
                tooltip.enabled =_inputManager.IsKeyboard;
            }
        }
        
        
    }
}

