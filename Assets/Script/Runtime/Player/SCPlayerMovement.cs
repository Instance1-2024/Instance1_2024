using Script.Runtime.InputSystem;
using UnityEngine;

namespace Script.Runtime.Player {
    public class SCPlayerMovement : MonoBehaviour {
        SCInputManager _inputManager => SCInputManager.Instance;

        private float _direction;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _frictionForce;
        
        [SerializeField] private float _jumpForce;
        
        [SerializeField] private float _turnTime;
        private float _time;

        GameObject _mesh;
        
        private Rigidbody _body;

        private bool _isMoving;
        
        private void Start() {
            _body = GetComponent<Rigidbody>();
            _mesh = transform.GetChild(0).gameObject;
            
            _inputManager.OnMoveEvent.Performed.AddListener(() => _isMoving = true);
            _inputManager.OnMoveEvent.Canceled.AddListener(() => _isMoving = false);
        }
        
        private void Update() {
            UpdateRotation();
        }
        
        private void FixedUpdate() {
            ClampSpeed();
            ApplyFriction();
            if (_isMoving) {
                Move();
            }
        }

        void Move() {
            if(_body.linearVelocity.magnitude < _maxSpeed) {
                Vector3 movement = (transform.right * _inputManager.MoveValue) * (_acceleration * Time.fixedDeltaTime);
                _body.linearVelocity += new Vector3(movement.x, 0, movement.z);
            }
        }

        
        
        void ClampSpeed() {
            Vector3 horizontalVelocity = new Vector3(_body.linearVelocity.x, 0, _body.linearVelocity.z);
            horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, _maxSpeed);
            if(!_body.isKinematic) {
                _body.linearVelocity = new Vector3(horizontalVelocity.x, _body.linearVelocity.y, horizontalVelocity.z);
                Debug.Log(horizontalVelocity);
            }
        }
        
        void ApplyFriction() {
            Vector3 friction = -_body.linearVelocity * (Time.fixedDeltaTime * _frictionForce);
            _body.AddForce(new Vector3(friction.x, 0, friction.y));
        }
        
        void UpdateRotation() {
            if(_isMoving && _inputManager.MoveValue != 0) {
                float targetAngle = _inputManager.MoveValue > 0 ? -90f : 90f;
                
                _time += Time.deltaTime / _turnTime;
                _time = Mathf.Clamp01(_time);
                float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle,_time);
                if( Mathf.Approximately(_time, 1)) {
                    _time = 0;
                }
                
                _mesh.transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            }
        }
        
        void Jump() {
            
        }
    }
}