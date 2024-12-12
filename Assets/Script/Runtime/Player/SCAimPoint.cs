using System;
using Script.Runtime.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using static Script.Runtime.SCUtils;

namespace Script.Runtime.Player {
    public class SCAimPoint : MonoBehaviour {
        
        private SCInputManager _inputManager => SCInputManager.Instance;
        public Vector3 ThrowPosition;
        [SerializeField] GameObject _point;

        private Vector2 _firstPos = Vector2.zero;
        private Vector2 _secondPos = Vector2.zero;
        
        float _angle;

        Camera _camera => Camera.main;
        
        public bool CanThrow;
        
        private void Update() {
            if (CanThrow) {
                ThrowPosition = GetThrowPosition();
                ClampAimPos();
                _point.transform.position = new Vector3(ThrowPosition.x, ThrowPosition.y, -1);
            }
            _point.SetActive(CanThrow);
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

            /*_secondPos.x = Mathf.Max(0, _secondPos.x + _inputManager.AimValue.x);
            _angle += _inputManager.AimValue.y;
            _angle = _angle % 360;
            if (_angle < 0) {
                _angle += 360;
            }

            return CalculatePositionByAngle(_firstPos, _secondPos, _angle);*/
        }

        /// <summary>
        /// Clamp the aim position to be next to the player
        /// </summary>
        private void ClampAimPos() {
            ThrowPosition = new Vector3(
                Mathf.Clamp(ThrowPosition.x, transform.position.x - 9, transform.position.x + 9),
                Mathf.Clamp(ThrowPosition.y, transform.position.y - 9, transform.position.y + 9),
                0
            );
        }
    }
}
