using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.ColorManagement {
    public class SCMultipleMeshChangeColor : MonoBehaviour, IChangeColor {
        private List<MeshRenderer> _meshRenderers;
        public Collider Collider { get; set; }
        [field:SerializeField]  public EColor CurrentColor { get; set; }
        public EColor GetColor() => CurrentColor;
        
        [SerializeField] private SColor _black;
        [SerializeField] private SColor _white;
        [SerializeField] private SColor _gray;

        void Awake() {
            _meshRenderers = transform.GetComponentsInChildren<MeshRenderer>().ToList();
        }
        
        private void Start() {
            ChangeColor(CurrentColor);
        }
        
        void ApplyColor(SColor color) {
            int layerValue = (int)Mathf.Log(color.Layer.value, 2);
            foreach (MeshRenderer meshRenderer in _meshRenderers) {
                meshRenderer.materials = color.Material.ToArray();
                meshRenderer.gameObject.layer = layerValue;
            }
            gameObject.layer = layerValue;
        }



        public void ChangeColor(EColor color) {
            switch (color) {
                case EColor.White:
                    ApplyColor(_white);
                    ExcludeLayer(_black.Layer);
                    break;
                case EColor.Black:
                    ApplyColor(_black);
                    ExcludeLayer(_white.Layer);
                    break;
                case EColor.Gray:
                    ApplyColor(_gray);
                    break;
            }
        }

        public void ExcludeLayer(LayerMask layer) {
            if(Collider == null) return;
            Collider.excludeLayers = layer.value;
        }
    }
}