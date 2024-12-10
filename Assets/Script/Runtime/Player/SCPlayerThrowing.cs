﻿using Script.Runtime.InputSystem;
using Script.Runtime.Pebble;
using UnityEngine;

namespace Script.Runtime.Player {
    public class SCPlayerThrowing : MonoBehaviour {
        SCInputManager _inputManager => SCInputManager.Instance;
        private SCPlayerHold _playerHold;
        private SCTrajectoryPredictor _trajectoryPredictor;

        private bool _StartedThrow;
        private Vector3 _throwPosition;
        [SerializeField]float _throwSpeed = 2.5f;
        [SerializeField] SCAimPoint _aimPoint;
        [SerializeField]GameObject _throwItem;
        Rigidbody _throwItemBody;

        private SProjectileData _projectile;
        
        SCPebble _pebbleComp;

        
        private void Start() {
            _playerHold = GetComponent<SCPlayerHold>();
            _trajectoryPredictor = GetComponent<SCTrajectoryPredictor>();
            
            _inputManager.OnStartThrowEvent.Performed.AddListener(StartThrow);
            _inputManager.OnThrowEvent.Performed.AddListener(Throw);
        }

        private void Update() {
            _aimPoint.CanThrow = _StartedThrow && _playerHold.IsHolding;
            if (_aimPoint.CanThrow) {
                UpdateProjectileData();
                Predict();
            }

            if (_pebbleComp != null) {
                if (_pebbleComp.IsColliding) {
                    RemoveItem();
                }
            }

            if (_playerHold.HoldItem == null ) {
                RemoveItem();
            }

            if (!_StartedThrow) {
                _trajectoryPredictor.SetTrajectoryVisible(false);
            }
        }
        
        
        /// <summary>
        /// When the player press the start throw button, it allows to throw
        /// </summary>
        void StartThrow() {
            _StartedThrow = !_StartedThrow && _playerHold.HoldItem != null;
        }

        /// <summary>
        /// When the player press the throw button, it throws the object if it can
        /// </summary>
        void Throw() {
            if(!_aimPoint.CanThrow) return;

            _throwPosition = _aimPoint.ThrowPosition;
            
            Throw(_throwPosition, _throwSpeed, _playerHold.HoldItem);
        }
        
        /// <summary>
        /// Throw the item to the throw position
        /// </summary>
        /// <param name="throwPosition"> The position to throw</param>
        /// <param name="throwSpeed"> The speed to throw</param>
        /// <param name="holdItem"> The item to throw</param>
        void Throw(Vector3 throwPosition, float throwSpeed, GameObject holdItem) {
            _throwItem = holdItem;
    
            _throwItem.SetActive(true);
            _throwItem.transform.SetParent(null);
     
            if (_throwItem.TryGetComponent(out _pebbleComp)) {
                _pebbleComp.IsColliding = false;
                
            }
            
            _throwItem.GetComponentInChildren<Collider>().enabled = true;


            
            Vector3 direction = (throwPosition - _throwItem.transform.position).normalized;
            
            _throwItemBody  = _throwItem.GetComponent<Rigidbody>();
            
            _throwItemBody.isKinematic = false;
            
            _throwItemBody.AddForce(direction * throwSpeed, ForceMode.Impulse);
        }

        /// <summary>
        /// ReActive the collision with the player to the throw item
        /// </summary>
        private void ActiveCollider() {
            if(_throwItem == null) return;
            if(_throwItem.TryGetComponent(out IThrowable throwable)) {
                throwable.GiveCollisions();
            }
        }

        /// <summary>
        /// Remove the item from the inventory
        /// </summary>
        void RemoveItem() {
            ActiveCollider();
            _playerHold.RemoveHoldImage();
            _trajectoryPredictor.SetTrajectoryVisible(false);
            _StartedThrow = false;
            _throwItem = null;
            _pebbleComp = null;
            _throwItemBody = null;
        }

        /// <summary>
        /// Update the projectile data for the trajectory
        /// </summary>
        private void UpdateProjectileData() {
            if(_playerHold.HoldItem == null) 
                return;
            _projectile = new SProjectileData{
                InitPos = _playerHold.HoldPoint.position,
                InitSpeed = _throwSpeed,
                Direction = (_aimPoint.ThrowPosition - _playerHold.HoldPoint.position).normalized,
                Mass = _playerHold.HoldItem.GetComponent<Rigidbody>().mass,
                Drag = 0.1f
            };
        }
        
        /// <summary>
        /// Predict the trajectory and show it
        /// </summary>
        void Predict() {
            _trajectoryPredictor.SetTrajectoryVisible(true);
            _trajectoryPredictor.PredictTrajectory(_projectile);
        }
        
    }
}