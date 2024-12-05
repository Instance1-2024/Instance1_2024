﻿using Script.Runtime.InputSystem;
using UnityEngine;

namespace Script.Runtime.Player {
    public class SCPlayerThrowing : MonoBehaviour {
        SCInputManager _inputManager => SCInputManager.Instance;

        private bool _StartedThrow;
        
        private SCPlayerHold _playerHold;
        public bool CanThrow;

        private Vector3 _ThrowPosition;
        
        Camera _camera => Camera.main;

        
        private void Start() {
            _playerHold = GetComponent<SCPlayerHold>();
            
            _inputManager.OnStartThrowEvent.Performed.AddListener(StartThrow);
            _inputManager.OnThrowEvent.Performed.AddListener(Throw);
        }

        private void Update() {
            CanThrow = _StartedThrow && _playerHold.CanHold;
            if (CanThrow) {
                Debug.Log( "Can Throw" );
            }
        }
        
        void StartThrow() {
            _StartedThrow = !_StartedThrow && _playerHold.HoldItem != null;
        }
        
        void Throw() {
            if(!_StartedThrow) return;

            _ThrowPosition = _camera.ScreenToWorldPoint( new(Input.mousePosition.x, Input.mousePosition.y, _camera.transform.position.z) );
            
            Debug.Log(_ThrowPosition);
            
            _StartedThrow = false;
        }
    }
}