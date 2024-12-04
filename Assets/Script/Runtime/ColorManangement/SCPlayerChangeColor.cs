using Script.Runtime.InputSystem;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.ColorManagement {
    public class SCPlayerChangeColor : SCColorChange {
        private SCInputManager _inputManager => SCInputManager.Instance;
        
        void Start() {
            ApplyColor(_white);
            _inputManager.OnChangeColorEvent.Performed.AddListener(OnChangeColor);
        }

        void OnChangeColor() {
            if (_oldColor == EColor.Black) {
                ChangeColor(EColor.White);
            }
            else if (_oldColor == EColor.White) {
                ChangeColor(EColor.Black);
            }
        }
    }
}