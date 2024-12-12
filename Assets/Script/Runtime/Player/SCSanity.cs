using Script.Runtime.ColorManagement;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Runtime.Player {
    public class SCSanity : MonoBehaviour {
        [SerializeField] Image _fillImage;
        [SerializeField] float _maxSanity = 10f;
        private SCChangeColor _changeColor;
        public float CurrentSanity;
        
        private void Start() {
            _changeColor = GetComponent<SCChangeColor>();
            CurrentSanity = _maxSanity;
            InvokeRepeating("AdjustSanity", 0.1f, 0.1f);
        }
        
        private void AdjustSanity() {
            if (ShouldReduceSanity()) {
                UpdateSanity(CurrentSanity - 0.1f);
            } else {
                UpdateSanity(CurrentSanity + 0.1f);
            }
        }

        private bool ShouldReduceSanity() {
            return _changeColor.GetColor() == SCEnum.EColor.Black;
        }
        
        public void  UpdateSanity(float sanity) {
            CurrentSanity = Mathf.Clamp(sanity, 0f, _maxSanity);
            if (CurrentSanity == 0) {
                GetComponent<SCPlayerRespawnAtCheckpoint>().OnRespawn();
            }
            _fillImage.fillAmount = CurrentSanity / _maxSanity;
        }
        
    }
}

