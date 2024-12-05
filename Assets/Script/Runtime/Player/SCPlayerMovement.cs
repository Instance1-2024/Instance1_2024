using System;
using System.Collections;
using System.Linq;
using Script.Runtime.InputSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Runtime.Player {
    public class SCPlayerMovement : MonoBehaviour {
        SCInputManager _inputManager => SCInputManager.Instance;

        [Header("Movement")]
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _frictionForce;
        private bool _isMoving;
        
        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        private bool _isGrounded;
        [SerializeField] private Vector3 _groundOffset;
        public LayerMask ColorGroundLayer;
        [SerializeField] LayerMask _groundLayer;
        [SerializeField] float _groundCheckRadius = 0.1f; 
        
        [Header("Rotation")]
        [SerializeField] private float _turnTime;
        private float _time;
        private float _direction;
        private float _oldY;
        
        enum FacingDirection { Right, Left }
        FacingDirection _facingDirection;
        
        GameObject _mesh;
        private Rigidbody _body;

        
        private void Start() {
            _body = GetComponent<Rigidbody>();
            _mesh = transform.GetChild(0).gameObject;
            
            _inputManager.OnMoveEvent.Performed.AddListener(() => _isMoving = true);
            _inputManager.OnMoveEvent.Canceled.AddListener(() => _isMoving = false);
            
            _inputManager.OnJumpEvent.Performed.AddListener(Jump);
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


            CheckGround();
        }
        

        
    #region MyRegion
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
            }
        }
        
        void ApplyFriction() {
            Vector3 friction = -_body.linearVelocity * (Time.fixedDeltaTime * _frictionForce);
            _body.AddForce(new Vector3(friction.x, 0, friction.y));
        }
    #endregion

    #region Rotation
    void UpdateRotation() {
        if (_inputManager.MoveValue > 0 && _facingDirection == FacingDirection.Left) {
            StartCoroutine(FlipSmoothly(0f));
        }
        else if (_inputManager.MoveValue < 0 && _facingDirection == FacingDirection.Right) {
            StartCoroutine(FlipSmoothly(-180f));
        }
    }

    private IEnumerator FlipSmoothly(float targetAngle) {
        _facingDirection = _facingDirection == FacingDirection.Left ? FacingDirection.Right : FacingDirection.Left;
                
        Quaternion startRotation = _mesh.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < _turnTime) {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _turnTime);
            _mesh.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        _mesh.transform.rotation = targetRotation;
    }
    #endregion        

        
        private void CheckGround() {
            Vector3 capsuleBottom = transform.position + _groundOffset;

            Collider[] hits = Physics.OverlapSphere(capsuleBottom, _groundCheckRadius, ColorGroundLayer);
            _isGrounded = hits.Any(hit => hit.transform != _mesh.transform && !hit.transform.IsChildOf(_mesh.transform));
            if (!_isGrounded) {
                hits = Physics.OverlapSphere(capsuleBottom, _groundCheckRadius, _groundLayer);
                _isGrounded = hits.Any(hit => hit.transform != _mesh.transform && !hit.transform.IsChildOf(_mesh.transform));
            }
        }    
    
        private void Jump() {
            if (_isGrounded) {
                _body.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                _isGrounded = false;
            }
        }
    }
}