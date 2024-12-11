using System;
using Script.Runtime.InputSystem;
using Script.Runtime.Player;
using UnityEngine;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.ColorManagement {
    public class ScChangePlayerColor : SCChangeColor {
        private SCInputManager _inputManager => SCInputManager.Instance;
        private SCPlayerMovement _playerMovement;
        private SCPlayerHold _playerHold;
        private CapsuleCollider _capsule;
        [SerializeField] private float _capsuleOffset;

        [SerializeField] private LayerMask _throwingLayer;

        void Start() {
            _inputManager.OnChangeColorEvent.Performed.AddListener(OnChangeColor);
            _playerMovement = GetComponent<SCPlayerMovement>();
            _playerHold = GetComponent<SCPlayerHold>();
            _capsule = GetComponent<CapsuleCollider>();
            ChangeColor(_oldColor);
        }

        /// <summary>
        /// When the player changes color, it changes the color
        /// </summary>
        void OnChangeColor() {
            if (_oldColor == EColor.Black) {
                ChangeColor(EColor.White);
            }
            else if (_oldColor == EColor.White) {
                ChangeColor(EColor.Black);
            }

            ChangeItemColor();
        }

        /// <summary>
        /// Changes the color and exclude the inverse layer from the collider and change the ground layer
        /// </summary>
        /// <param name="color"> The color to apply</param>
        public override void ChangeColor(EColor color) {
            switch (color) {
                case EColor.White:
                    ApplyColor(_white);
                    ExcludeLayer(_black);
                    ChangeGroundLayer(_white);

                    break;
                case EColor.Black:
                    ApplyColor(_black);
                    ExcludeLayer(_white);
                    ChangeGroundLayer(_black);

                    break;
                case EColor.Gray:
                    ApplyColor(_gray);

                    break;
            }

            _oldColor = color;
            DetectWall();
        }

        public override void ExcludeLayer(SColor color) {
            _collider.excludeLayers = color.Layer.value | _throwingLayer.value;
        }

        /// <summary>
        /// Changes the ground layer
        /// </summary>
        /// <param name="color.Layer"> the layer to apply</param>
        void ChangeGroundLayer(SColor color) {

            _playerMovement.ColorGroundLayer = color.Layer.value;
        }

        void ChangeItemColor() {
            if (!_playerHold.IsHolding) return;
            if (_playerHold.HoldItem.TryGetComponent(out SCChangeColor _changeColor)) {
                if (_changeColor.GetColor() == EColor.Gray) return;
                _changeColor.ChangeColor(_oldColor);
            }
        }

        void DetectWall() {
            Vector3 capsuleCenter = _capsule.bounds.center;
            float capsuleRadius = _capsule.radius * 0.9f; // Réduction de 10%
            float capsuleHeight = _capsule.height * 0.9f; // Réduction de 10%
        
            // Calculer les points de la capsule
            Vector3 point1 = capsuleCenter + Vector3.up * (capsuleHeight / 2 - capsuleRadius);
            Vector3 point2 = capsuleCenter - Vector3.up * (capsuleHeight / 2 - capsuleRadius);
        
            // Direction et distance du cast
            Vector3 direction = transform.forward;


            RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, capsuleRadius, direction, 10f);
            foreach (RaycastHit hit in hits) {
                if (hit.transform.CompareTag("Platform")) {
                    if (hit.transform.parent.GetComponent<SCChangeColor>().GetColor() == GetColor()) {
                        GetComponent<SCPlayerRespawnAtCheckpoint>().OnRespawn();
                    }
                }
            }
        }


    }
}