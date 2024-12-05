using Script.Runtime.InputSystem;
using UnityEngine;

namespace Script.Runtime.Player {
    public class SCPlayerThrowing : MonoBehaviour {
        SCInputManager _inputManager => SCInputManager.Instance;

        private bool _StartedThrow;
        
        
        private void Start() {
            _inputManager.OnStartThrowEvent.Performed.AddListener(StartThrow);
            _inputManager.OnThrowEvent.Performed.AddListener(Throw);
        }
        
        
        void StartThrow() {
            _StartedThrow = !_StartedThrow;
        }
        
        void Throw() {
            if(!_StartedThrow) return;
            
            _StartedThrow = false;
            Debug.Log("AAAAAA");
        }
    }
}