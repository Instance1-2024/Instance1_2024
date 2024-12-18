using Script.Runtime.ColorManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Runtime.Player {
    public class SCSanity : MonoBehaviour {
        [SerializeField] Image _fillImage;
        [SerializeField] float _maxSanity = 10f;
        private ScPlayerChangeColor _changeColor;
        public float CurrentSanity;

        [Header("Sound Related")]
        private bool _isSoundPlaying = false;
        [SerializeField] private AudioClip _fillClip;
        [SerializeField] private AudioSource _fillAudioSource;
        
        private void Start() {
            _changeColor = GetComponent<ScPlayerChangeColor>();
            CurrentSanity = _maxSanity;
            InvokeRepeating("AdjustSanity", 0.1f, 0.1f);
        }
        
        private void AdjustSanity() {
            if (ShouldReduceSanity()) {
                UpdateSanity(CurrentSanity - 0.1f);
                if(!_isSoundPlaying)
                {
                    if (_fillAudioSource == null) return;

                    StopAllCoroutines();
                    _isSoundPlaying = true;
                    _fillAudioSource.clip = _fillClip;
                    _fillAudioSource.volume = 1;
                    _fillAudioSource.Play();
                }
            } else {
                UpdateSanity(CurrentSanity + 0.1f);

                if(_isSoundPlaying)
                {
                    StopAllCoroutines();
                    _isSoundPlaying = false;
                    StartCoroutine(DecreaseSound());
                }
            }
        }

        private IEnumerator DecreaseSound()
        {
            if (_fillAudioSource == null) yield return null;

            float timeElapsed = 0f;

            while (timeElapsed < 1)
            {
                // Interpoler la valeur du volume
                _fillAudioSource.volume -= Time.deltaTime;
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            _fillAudioSource.volume = 0;
            yield return null;
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

