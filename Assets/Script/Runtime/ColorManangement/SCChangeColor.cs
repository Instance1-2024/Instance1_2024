using System;
using UnityEngine;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.ColorManagement {

    public class SCColorChange : MonoBehaviour {
        protected MeshFilter _meshFilter;
        protected MeshRenderer _meshRenderer;

        protected Collider _collider;
        
        [SerializeField] protected EColor _oldColor;
        
        [SerializeField] protected  SColor _black;
        [SerializeField] protected SColor _white;
        [SerializeField] protected SColor _gray;
        
        void Awake() {
            _meshRenderer = transform.GetComponentInChildren<MeshRenderer>();
            _meshFilter = transform.GetComponentInChildren<MeshFilter>();
            _collider = transform.GetComponentInChildren<Collider>();
        }

        private void Start() {
            ChangeColor(_oldColor);
        }

        public void ApplyColor(SColor color) {
            _meshFilter.mesh = color.Mesh;
            _meshRenderer.material = color.Material;
            int layerValue = (int)Mathf.Log(color.Layer.value, 2);
            gameObject.layer = layerValue;
            _meshFilter.gameObject.layer = layerValue;
        }
        
       protected void ExcludeLayer(SColor color) {
            _collider.excludeLayers = color.Layer.value;
        }
        
        public virtual void ChangeColor(EColor color) {
            switch (color) {
                case EColor.White:
                    ApplyColor(_white);
                    ExcludeLayer(_black);
                    break;
                case EColor.Black:
                    ApplyColor(_black);
                    ExcludeLayer(_white);
                    break;
                case EColor.Gray:
                    ApplyColor(_gray);
                    break;
            }

            _oldColor = color;
        }

        [Serializable]
        public struct SColor {
            public Mesh Mesh;
            public Material Material;
            public LayerMask Layer;
        }
    }
}