using UnityEngine;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.ColorManagement {
    public class SCMultipleMeshChangeColor : MonoBehaviour, IChangeColor {
        public EColor CurrentColor { get; set; }
        public Collider Collider { get; set; }
        public void ChangeColor(EColor color) {
            throw new System.NotImplementedException();
        }

        public void ExcludeLayer(LayerMask layer) {
            throw new System.NotImplementedException();
        }
    }
}