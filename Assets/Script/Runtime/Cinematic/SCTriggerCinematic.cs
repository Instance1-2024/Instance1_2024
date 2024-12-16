using Script.Runtime.InputSystem;
using Script.Runtime.InputSystem.InputTooltip;
using Script.Runtime.Player;
using TMPro;
using UnityEngine;

public class TriggerCinematic : MonoBehaviour {
    public Animation animationCinematic;
    public bool destroyTrigger;
    [SerializeField, Multiline] private string _text;
    private SCInputManager _inputManager => SCInputManager.Instance;
    private SCPlayerMovement _playerMovement;
    
    private TextMeshProUGUI _tooltip;
    
    [SerializeField] SCInputTooltipText _keyboardTooltip;
    [SerializeField] SCInputTooltipText _gamepadTooltip;
    
    bool _hasBeenTriggered;
    
    
    private void OnTriggerEnter(Collider other) {
        if(_hasBeenTriggered) return;
        if (other.gameObject.CompareTag("Player")) {
            _playerMovement = other.gameObject.GetComponent<SCPlayerMovement>();
            _playerMovement.SetVelocityLock(true);
            
            _tooltip = other.gameObject.GetComponentInChildren<SCPlayerTooltip>().GetTooltipText();
            _inputManager.IsInputActive = false;
            _inputManager.MoveValue = 0f;
            animationCinematic.Play(animationCinematic.clip.name);
            destroyTrigger = true;
        }
    }
    private void OnTriggerStay(Collider other) {
        if(_hasBeenTriggered) return;
        if (other.gameObject.CompareTag("Player")) {
            if (destroyTrigger && !animationCinematic.isPlaying) {
                _playerMovement.SetVelocityLock(false);
                _tooltip.gameObject.SetActive(true);
                _inputManager.IsInputActive = true;
                SetToolTipText();
            }
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if(_hasBeenTriggered) return;
        if (other.gameObject.CompareTag("Player")) {
            _tooltip.gameObject.SetActive(false);
            _hasBeenTriggered = true;
            gameObject.SetActive(false);
            SetNull();
        }
    }
    
    void SetToolTipText() {
        _tooltip.text = _text.Replace("{binding}", GetBind());;
    }

    string GetBind() {
        return _inputManager.IsKeyboard ? _keyboardTooltip.BindingText : _gamepadTooltip.BindingText;
    }

    void SetNull() {
        _tooltip = null;
        _playerMovement = null;
    }
    

}
