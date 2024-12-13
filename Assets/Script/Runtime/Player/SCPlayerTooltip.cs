using System;
using TMPro;
using UnityEngine;

namespace Script.Runtime.Player {
    public class SCPlayerTooltip : MonoBehaviour {
        [SerializeField] TextMeshProUGUI _tooltipText;

        private void Start() {
            _tooltipText.gameObject.SetActive(false);
        }

        public TextMeshProUGUI GetTooltipText() => _tooltipText;
    }
}
