using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Runtime.Player {
    public class SCPlayerTooltip : MonoBehaviour {
        [SerializeField] TextMeshProUGUI _tooltipText;
        [SerializeField] Image _tooltipImage;

        private void Start() {
            _tooltipText.gameObject.SetActive(false);
            _tooltipImage.gameObject.SetActive(false);
        }

        public TextMeshProUGUI GetTooltipText() => _tooltipText;
        public Image GetTooltipImage() => _tooltipImage;
        
    }
}
