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

                if (_inputManager.IsKeyboard){
                   ThrowPosition = _camera.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y,-_camera.transform.position.z));
                }
                else {
                    ThrowPosition = Vector2To3Y(ThrowPosition,_inputManager.AimValue);
                }
                ThrowPosition = new Vector3(
                    Mathf.Clamp(ThrowPosition.x, transform.position.x - 21, transform.position.x + 21),
                    Mathf.Clamp(ThrowPosition.y, transform.position.y - 12, transform.position.y + 12),
                    0
                );

            }


        }


    }
}
