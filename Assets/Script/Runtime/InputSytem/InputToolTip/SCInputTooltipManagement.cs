using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Runtime.InputSystem.InputTooltip {
    public class SCInputTooltipManagement : MonoBehaviour {
        [SerializeField, Multiline] protected string _tooltipText;
        protected SCInputManager _inputManager => SCInputManager.Instance;
        protected TextMeshProUGUI _tooltip;
        
        [SerializeField]  protected SCInputTooltipText _keyboardTooltip;
        [SerializeField]  protected SCInputTooltipText _gamepadTooltip;
        
        [SerializeField]  protected Image _tooltipImage;
        
        [SerializeField]  protected Sprite _gamepadSprite;
        [SerializeField]  protected Sprite _keyboardSprite;
        
        protected void SetToolTipText() {
            _tooltip.text = _tooltipText.Replace("{binding}", GetBind());
            _tooltipImage.sprite = _inputManager.IsKeyboard ? _keyboardSprite : _gamepadSprite;
        }

        protected string GetBind() {
            return _inputManager.IsKeyboard ? _keyboardTooltip.BindingText : _gamepadTooltip.BindingText;
        }
    }
}

