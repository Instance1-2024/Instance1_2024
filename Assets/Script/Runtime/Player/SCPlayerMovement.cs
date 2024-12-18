using System.Collections;
using System.Linq;
using Script.Runtime.InputSystem;
using UnityEngine;
using static Script.Runtime.SCUtils;

namespace Script.Runtime.Player {
    public class SCPlayerMovement : MonoBehaviour {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        SCInputManager _inputManager => SCInputManager.Instance;

        [Header("Animators")]
        public Animator CurrentAnimator;
        public Animator WhiteAnimator;
        public Animator BlackAnimator;
        
        [Header("SoundManager")]
        public SCSoundManager CurrentSoundManager;
        public SCSoundManager WhiteSoundManager;
        public SCSoundManager BlackSoundManager;
        
        private float _walkSoundTimer;

        [Header("Movement")]
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _frictionForce;
        private bool _isMoving;
        private Vector3 _horizontalVelocity;
        [SerializeField] private float _frictionDuration;
        private float _frictionTime;
        
        
        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        private bool _isGrounded;
        private bool _isBuffered;
        private bool _haveToJump;
        public LayerMask ColorGroundLayer;
        [SerializeField] LayerMask _groundLayer;

        [SerializeField] private Transform _groundCheck;
        [SerializeField] float _groundCheckRadius;
        [SerializeField]private float _boxHeight;
        [SerializeField]private float _boxWidth;
        [SerializeField] float _fallSpeed;
        

        [Header("Rotation")]
        [SerializeField] private float _turnDuration;
        private float _turnTime;
        private float _direction;
        private float _oldY;
        
        enum FacingDirection { Right, Left }
        FacingDirection _facingDirection;
        
        [SerializeField] GameObject _comp;
        private Rigidbody _body;

        private SCPhysicMaterialManager _materialManager;
        
        private bool _isInCinematic;
        private bool _isDeath;

        private void Start() {
            _body = GetComponent<Rigidbody>();
            _materialManager = GetComponent<SCPhysicMaterialManager>();
           
            _inputManager.OnMoveEvent.Performed.AddListener(() => _isMoving = true);
            _inputManager.OnMoveEvent.Canceled.AddListener(() => _isMoving = false);
            
            _inputManager.OnJumpEvent.Performed.AddListener(Jump);
        }

        private void Update() {
            UpdateRotation();

            if (_isMoving) {
                _walkSoundTimer -= Time.deltaTime;
                if (_walkSoundTimer <= 0) {
                    CurrentSoundManager.PlayWalkSound();
                    _walkSoundTimer = 2f;
                }
            }
        }

        private void FixedUpdate() {
            ClampSpeed();
            ApplyFriction();
            if (_isMoving) {
                CurrentAnimator.SetBool(IsWalking, true);
                Move();
            }
            else {
                CurrentAnimator.SetBool(IsWalking, false);
            }

            if (_isGrounded) {
                if (_frictionTime < _frictionDuration) {
                    _frictionTime += Time.deltaTime;
                    
                    _materialManager.ApplyFrictions(Mathf.Lerp(0f, 0.6f, _frictionTime/_frictionDuration));
                }

                _materialManager.ApplyFrictionCombine(PhysicsMaterialCombine.Average);
            } else {
                _frictionTime = 0;
                _materialManager.ApplyFrictions(0f);
                _materialManager.ApplyFrictionCombine(PhysicsMaterialCombine.Minimum);
                _body.AddForce(Vector3.down * _fallSpeed, ForceMode.Acceleration);
                /*Debug.Log(_horizontalVelocity.magnitude);*/
            }


            SetVelocityLock(_isDeath || _isInCinematic);
                
            
            CheckGround();


            if (_haveToJump) {
                Jump();
            }
        }
        

        
    #region Movement
        
        /// <summary>
        /// Move the player
        /// </summary>
        void Move() {
            if(_horizontalVelocity.magnitude < _maxSpeed) {
                Vector3 movement = (transform.right * _inputManager.MoveValue) * (_acceleration * Time.fixedDeltaTime);
                _body.linearVelocity += new Vector3(movement.x, 0, movement.z);
            }
        }

        /// <summary>
        /// Limit the Player speed
        /// </summary>
        void ClampSpeed() {
            _horizontalVelocity = new Vector3(_body.linearVelocity.x, 0, _body.linearVelocity.z);
            _horizontalVelocity = Vector3.ClampMagnitude(_horizontalVelocity, _maxSpeed);
            if(!_body.isKinematic) {
                _body.linearVelocity = new Vector3(_horizontalVelocity.x, _body.linearVelocity.y, _horizontalVelocity.z);
            }
        }
        
        /// <summary>
        /// Apply the Friction of the player
        /// </summary>
        void ApplyFriction() {
            Vector3 friction = -_body.linearVelocity * (Time.fixedDeltaTime * _frictionForce);
            _body.AddForce(new Vector3(friction.x, 0, friction.y));
        }
        

        public void SetVelocityLock(bool lockVelocity) {
            _inputManager.IsInputActive = !lockVelocity;
            CurrentAnimator.enabled = !lockVelocity;
            _body.constraints = lockVelocity ? RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            if (lockVelocity) {
                _inputManager.MoveValue = 0f;
                _body.linearVelocity = Vector3.zero;
                CurrentAnimator.SetBool(IsWalking,false);
                CurrentAnimator.SetBool(IsJumping,false);
            }
        }
        
        public void SetInCinematic(bool value) {
            _isInCinematic = value;
        }
        
        public void SetDeath(bool value) {
            _isDeath = value;
        }
        
    #endregion

    #region Rotation
    
        /// <summary>
        /// Choose rotation by the direction of the player
        /// </summary>
        void UpdateRotation() {
            if (_inputManager.MoveValue > 0 && _facingDirection == FacingDirection.Left) {
                StartCoroutine(FlipSmoothly(0f));
            }
            else if (_inputManager.MoveValue < 0 && _facingDirection == FacingDirection.Right) {
                StartCoroutine(FlipSmoothly(-180f));
            }
        }

        
        /// <summary>
        /// Flip the player smoothly    
        /// </summary>
        /// <param name="targetAngle"> The angle to flip </param>
        /// <returns></returns>
        private IEnumerator FlipSmoothly(float targetAngle) {
            _facingDirection = _facingDirection == FacingDirection.Left ? FacingDirection.Right : FacingDirection.Left;
                    
            Quaternion startRotation = _comp.transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            float elapsedTime = 0f;

            while (elapsedTime < _turnDuration) {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / _turnDuration);
                _comp.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
                yield return null;
            }

            _comp.transform.rotation = targetRotation;
        }
    #endregion


    #region Jump
    
        /// <summary>
        /// Detect if the player is on the ground
        /// </summary>
        private void CheckGround() {
            Vector3 boxCenter = _groundCheck.position;
            Vector3 boxSize = new (_boxWidth, _boxHeight, _boxWidth);
            Collider[] hits = Physics.OverlapBox(boxCenter, boxSize / 2, Quaternion.identity, ColorGroundLayer | _groundLayer);

            _isBuffered = hits.Any(hit => !IsMyself(hit.transform, transform));
        }
        
        /// <summary>
        /// Jump if the player is on the ground
        /// </summary>
        private void Jump() {
            if (_isGrounded) {
                CurrentAnimator.SetBool(IsJumping, true);
                if (CurrentAnimator.transform.GetComponent<SCSoundManager>())
                    CurrentAnimator.transform.GetComponent<SCSoundManager>().PlayJumpUpSound();
                _body.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                _isGrounded = false;
                _haveToJump = false;
                _isBuffered = false;
            }else if (_isBuffered) {
                _haveToJump = true;
            }
            
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Vector3 boxCenter = _groundCheck.position;
            Vector3 boxSize = new (_boxWidth, _boxHeight, _boxWidth);
            Gizmos.DrawWireCube(boxCenter, boxSize);
        }

        #endregion


        private void OnCollisionEnter(Collision other) {
            
            Collider[] hits = Physics.OverlapSphere(_groundCheck.position, _groundCheckRadius, ColorGroundLayer | _groundLayer);
            _isGrounded = hits.Any(hit => !IsMyself(hit.transform, transform));
            if (_isGrounded) {
                CurrentSoundManager.PlayJumpDownSound();
                CurrentAnimator.SetBool(IsJumping, false);
                if (CurrentAnimator.transform.GetComponent<SCSoundManager>())
                    CurrentAnimator.transform.GetComponent<SCSoundManager>().PlayJumpDownSound();
            }
        }
    }
}