using Script.Runtime.InputSystem;
using Script.Runtime.Player;
using UnityEngine;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.ColorManagement {
    public class ScPlayerChangeColor : MonoBehaviour,IChangeColor {
        [field:SerializeField] public EColor CurrentColor { get; set; }
        public Collider Collider { get; set; }
        
        private SCInputManager _inputManager => SCInputManager.Instance;
        private SCPlayerMovement _playerMovement;
        private SCPlayerHold _playerHold;
        private CapsuleCollider _capsule;

        [SerializeField] private LayerMask _throwingLayer;
        [SerializeField] private LayerMask _layersThatCollide;
        
        [SerializeField] LayerMask _black;
        [SerializeField]  GameObject _blackBody;
        
        [SerializeField] LayerMask _white;
        [SerializeField] GameObject _whiteBody;

        [SerializeField] private bool _isLevel1 = false;
        private bool _canChangeColor = false;

        public void CanChangeColor(bool value)
        {
            if(!_canChangeColor)
                _canChangeColor = value;
        }

        private void Awake() {
            Collider = GetComponent<Collider>();

            if (!_isLevel1)
                _canChangeColor = true;
        }

        void Start() {
            _inputManager.OnChangeColorEvent.Performed.AddListener(OnChangeColor);
            _playerMovement = GetComponent<SCPlayerMovement>();
            _playerHold = GetComponent<SCPlayerHold>();
            _capsule = Collider as CapsuleCollider;
            ChangeColor(CurrentColor);
        }

        /// <summary>
        /// When the player changes color, it changes the color
        /// </summary>
        void OnChangeColor()
        {
            if (!_canChangeColor)
                return;
            if (CurrentColor == EColor.Black) {
                ChangeColor(EColor.White);
            }
            else if (CurrentColor == EColor.White) {
                ChangeColor(EColor.Black);
            }

            ChangeItemColor();
            DetectWall();
        }

        /// <summary>
        /// Changes the color and exclude the inverse layer from the collider and change the ground layer
        /// </summary>
        /// <param name="color"> The color to apply</param>
        public void ChangeColor(EColor color) {

            switch (color) {
                case EColor.White:
                    ExcludeLayer(_black);
                    ChangeGroundLayer(_white);
                    ApplyColor(true);

                    break;
                case EColor.Black:
                    ExcludeLayer(_white);
                    ChangeGroundLayer(_black);
                    ApplyColor(false);

                    break;
                case EColor.Gray:
                    break;
            }

            CurrentColor = color;
        }

        void ApplyColor(bool isWhite) {
            _playerMovement.CurrentAnimator = isWhite ? _playerMovement.WhiteAnimator : _playerMovement.BlackAnimator;
            _playerMovement.CurrentSoundManager = isWhite ? _playerMovement.WhiteSoundManager : _playerMovement.BlackSoundManager;
            _whiteBody.SetActive(isWhite);
            _blackBody.SetActive(!isWhite);
        }

        public EColor GetColor() => CurrentColor;
        
        public void ExcludeLayer(LayerMask layer) {
            Collider.excludeLayers = layer.value | _throwingLayer.value;
        }

        /// <summary>
        /// Changes the ground layer
        /// </summary>
        /// <param name="layer"> the layer to apply</param>
        void ChangeGroundLayer(LayerMask layer) {
            _playerMovement.ColorGroundLayer = layer.value;
        }

        void ChangeItemColor() {
            if (!_playerHold.IsHolding) return;
            if (_playerHold.HoldItem.TryGetComponent(out SCChangeColor _changeColor)) {
                if (_changeColor.GetColor() == EColor.Gray) return;
                _changeColor.ChangeColor(CurrentColor);
            }
        }

        void DetectWall() {
            Vector3 capsuleCenter = _capsule.bounds.center;
            float capsuleRadius = _capsule.radius * 0.9f;
            float capsuleHeight = _capsule.height * 0.9f;
            
            Vector3 point1 = capsuleCenter + Vector3.up * (capsuleHeight / 2 - capsuleRadius);
            Vector3 point2 = capsuleCenter - Vector3.up * (capsuleHeight / 2 - capsuleRadius);

            Vector3 direction = transform.forward;

            RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, capsuleRadius, direction, 10f, _layersThatCollide);
            foreach (RaycastHit hit in hits) {
                if (hit.transform.CompareTag("Platform")) {
                    if (hit.transform.GetComponent<SCChangeColor>().GetColor() == GetColor()) {
                        GetComponent<SCPlayerRespawnAtCheckpoint>().OnRespawn();
                        return;
                    }
                }
            }
        }
    }
}