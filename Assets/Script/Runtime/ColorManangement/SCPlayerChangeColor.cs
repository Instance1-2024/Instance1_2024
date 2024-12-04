using Script.Runtime.InputSystem;
using UnityEngine;
using static Script.Runtime.SCEnum;
namespace Script.Runtime.ColorManagement {
    public class SCPlayerChangeColor : MonoBehaviour {
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private SCInputManager _inputManager => SCInputManager.Instance;

        [SerializeField] EColor _oldColor;

        [SerializeField] SColor _black;
        [SerializeField] SColor _white;
        [SerializeField] SColor _gray;
        
        void Start() {
            _meshFilter = transform.GetComponentInChildren<MeshFilter>();
            _meshRenderer = transform.GetComponentInChildren<MeshRenderer>();
            
            ApplyColor(_white);
            
            _inputManager.OnChangeColorEvent.Performed.AddListener(OnChangeColor);
        }

        void OnChangeColor() {
            Debug.Log("S");
            if (_oldColor == EColor.Black) {
                Debug.Log("Go to White");
                ChangeColor(EColor.White);
            }
            else if (_oldColor == EColor.White) {
                Debug.Log("Go to Black");
                ChangeColor(EColor.Black);
            }
        }
        
        public void ChangeColor(EColor color) {
            if (_oldColor == color) {
                Debug.Log( _oldColor);
                return;
            }
                
                
            
            switch (color) {
                case EColor.White:
                    Debug.Log("WHITE");
                    ApplyColor(_white);
                    break;
                case EColor.Black:
                    Debug.Log("BLACK");
                    ApplyColor(_black);
                    break;
                case EColor.Gray:
                    ApplyColor(_gray);
                    break;
            }

            _oldColor = color;
        }
        
        private void ApplyColor(SColor color) {
            _meshFilter.mesh = color.Mesh;
            _meshRenderer.material = color.Material;
            int layerValue = (int)Mathf.Log(color.Layer.value, 2);
            gameObject.layer = layerValue;
        }
        
        [System.Serializable]
        public struct SColor {
            public Mesh Mesh;
            public Material Material;
            public LayerMask Layer;
        }
    }
}