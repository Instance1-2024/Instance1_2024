using UnityEngine;
using static Script.Runtime.SCEnum;
namespace Script.Runtime.ColorManagment {
    public class SCPlayerChangeColor : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        [SerializeField] private Mesh _meshBlack, _meshWhite, _meshGrey;
        [SerializeField] private LayerMask _layerBlack, _layerWhite, _layerGrey;
        void Start()
        {
            _meshFilter = transform.GetComponent<MeshFilter>();
            ChangeColor(EColor.White);
        }

        public void ChangeColor(EColor color)
        {
            switch (color)
            {
                case EColor.White:
                    ApplyColor(_meshWhite, _layerWhite);
                    break;
                case EColor.Black:
                    ApplyColor(_meshBlack, _layerBlack);
                    break;
                case EColor.Grey:
                    ApplyColor(_meshGrey, _layerGrey);
                    break;
            }

        }
        private void ApplyColor(Mesh mesh, LayerMask layer)
        {
            _meshFilter.mesh = mesh;
            int layerValue = (int)Mathf.Log(layer.value, 2);
            gameObject.layer = layerValue;
        }
    }
}