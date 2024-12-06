using System;
using System.Collections.Generic;
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

        public EColor GetColor() => _oldColor;
        
        
        /// <summary>
        /// Applies the mesh, the material and the layer to the object
        /// </summary>
        /// <param name="color.Mesh"> The mesh to apply</param>
        /// <param name="color.Material"> The material to apply</param>
        /// <param name="color.Layer"> The layer to apply</param>
        public void ApplyColor(SColor color) {
            _meshFilter.mesh = color.Mesh;
            _meshRenderer.materials = color.Material.ToArray();
            int layerValue = (int)Mathf.Log(color.Layer.value, 2);
            gameObject.layer = layerValue;
            _meshFilter.gameObject.layer = layerValue;
        }
        
        /// <summary>
        /// Excludes the layer from the collider
        /// </summary>
        /// <param name="color.Layer"> The layer to exclude</param>
       protected void ExcludeLayer(SColor color) {
            _collider.excludeLayers = color.Layer.value;
        }
        
        /// <summary>
        /// Changes the color and exclude the inverse color layer from the collider
        /// </summary>
        /// <param name="color"> The color to apply</param>
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

        /// <summary>
        /// The structure that contains the mesh, the material and the layer to apply to the object
        /// </summary>
        /// <param name="Mesh">The mesh to apply</param>
        /// <param name="Material">The material to apply</param>
        /// <param name="Layer">The layer to apply</param>
        [Serializable]
        public struct SColor {
            public Mesh Mesh;
            public List<Material> Material;
            public LayerMask Layer;
        }
    }
}