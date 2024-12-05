using Script.Runtime.InputSystem;
using Script.Runtime.Player;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.ColorManagement {
    public class SCPlayerChangeColor : SCColorChange {
        private SCInputManager _inputManager => SCInputManager.Instance;
        private SCPlayerMovement _playerMovement;
        
        void Start() {
            _inputManager.OnChangeColorEvent.Performed.AddListener(OnChangeColor);
            _playerMovement = GetComponent<SCPlayerMovement>();
            ChangeColor(_oldColor);
        }

        void OnChangeColor() {
            if (_oldColor == EColor.Black) {
                ChangeColor(EColor.White);
            }
            else if (_oldColor == EColor.White) {
                ChangeColor(EColor.Black);
            }
        }
        
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
        }

        void ChangeGroundLayer(SColor color) {
            _playerMovement.ColorGroundLayer = color.Layer.value;
        }
    }
}