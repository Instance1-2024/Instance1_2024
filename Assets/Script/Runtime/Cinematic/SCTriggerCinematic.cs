using Script.Runtime.ColorManagement;
using Script.Runtime.InputSystem;
using Script.Runtime.InputSystem.InputTooltip;
using Script.Runtime.Player;
using TMPro;
using UnityEngine;

public class TriggerCinematic : SCInputTooltipManagement {
    public Animation animationCinematic;
    public bool destroyTrigger;

    private SCPlayerMovement _playerMovement;
    

    
    [SerializeField] GameObject HUD;
    
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
                HUD.SetActive(true);
                _playerMovement.SetVelocityLock(false);
                _tooltip.gameObject.SetActive(true);
                _inputManager.IsInputActive = true;
                SetToolTipText();

                if (other.TryGetComponent(out ScPlayerChangeColor playerChangeColor))
                {
                    playerChangeColor.CanChangeColor(true);
                }
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
    


    void SetNull() {
        _tooltip = null;
        _playerMovement = null;
    }
    

}
