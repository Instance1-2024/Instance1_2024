using System;
using Script.Runtime.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using static Script.Runtime.SCUtils;

namespace Script.Runtime.Player {
    public class SCAimPoint : MonoBehaviour {
        
        private SCInputManager _inputManager => SCInputManager.Instance;
        public Vector3 ThrowPosition;

        private Image _image;
        private RectTransform _rectTransform;
        Camera _camera => Camera.main;
        
        public bool CanThrow;
        
        private void Start() {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
            _image.enabled = false;
        }


        private void Update() {
            if (CanThrow) { 
                /*_image.enabled = true;*/
                if (_inputManager.IsKeyboard){
                   ThrowPosition = _camera.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y,-_camera.transform.position.z));
                }
                else {
                    ThrowPosition = Vector2To3Y(ThrowPosition,_inputManager.AimValue);
                }
                ThrowPosition = new Vector3(
                    Mathf.Clamp(ThrowPosition.x, -21, 21),
                    Mathf.Clamp(ThrowPosition.y, -12, 12),
                    0
                );
                
                /*_rectTransform.position = ThrowPosition;*/
            }
            else {
                /*_image.enabled = false;*/
            }
        }


    }
}
