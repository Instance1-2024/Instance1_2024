using System;
using Script.Runtime.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using static Script.Runtime.SCUtils;

namespace Script.Runtime.Player {
    public class SCAimPoint : MonoBehaviour {
        
        private SCInputManager _inputManager => SCInputManager.Instance;
        public Vector3 ThrowPosition;
        

        Camera _camera => Camera.main;
        
        public bool CanThrow;
        
        private void Update() {
            if (CanThrow) {
                ThrowPosition = GetThrowPosition();
                ClampAimPos();
            }
        }
        
        /// <summary>
        /// Get the position chosen by the player for throwing if it's a keyboard or a gamepad
        /// </summary>
        /// <returns>The position</returns>
        private Vector3 GetThrowPosition() {
            if (_inputManager.IsKeyboard){
                 return _camera.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y,-_camera.transform.position.z));
            }
            
            return Vector2To3YX(ThrowPosition,_inputManager.AimValue);
        }

        /// <summary>
        /// Clamp the aim position to be next to the player
        /// </summary>
        private void ClampAimPos() {
            ThrowPosition = new Vector3(
                Mathf.Clamp(ThrowPosition.x, transform.position.x - 21, transform.position.x + 21),
                Mathf.Clamp(ThrowPosition.y, transform.position.y - 12, transform.position.y + 12),
                0
            );
        }
    }
}
