using System;
using System.Collections.Generic;
using UnityEngine;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.ColorManagement {

    public class SCChangeColor : MonoBehaviour, IChangeColor {
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        
        [field:SerializeField] public EColor CurrentColor { get; set; }
        public Collider Collider { get; set; }
        
        [SerializeField] private  SColor _black;
        [SerializeField] private SColor _white;
        [SerializeField] private SColor _gray;
        
        void Awake() {
            _meshRenderer = transform.GetComponentInChildren<MeshRenderer>();
            _meshFilter = transform.GetComponentInChildren<MeshFilter>();
            Collider = transform.GetComponentInChildren<Collider>();
        }

        private void Start() {
            ChangeColor(CurrentColor);
        }


        public EColor GetColor() => CurrentColor;

        /// <summary>
        /// Applies the mesh, the material and the layer to the object
        /// </summary>
        /// <param name="color">The struct that contains the mesh, the material and the layer to apply to the object</param>
        /// <param name="color.Mesh"> The mesh to apply</param>
        /// <param name="color.Material"> The material to apply</param>
        /// <param name="color.Layer"> The layer to apply</param>
        private void ApplyColor(SColor color) {
            _meshFilter.mesh = color.Mesh;
            _meshRenderer.materials = color.Material.ToArray();
            int layerValue = (int)Mathf.Log(color.Layer.value, 2);
            gameObject.layer = layerValue;
            _meshFilter.gameObject.layer = layerValue;
        }
        
        /// <summary>
        /// Changes the color and exclude the inverse color layer from the collider
        /// </summary>
        /// <param name="color"> The color to apply</param>
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

            CurrentColor = color;
        }

        public void ExcludeLayer(LayerMask layer) {
            if(Collider == null) return;
            Collider.excludeLayers = layer.value;
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