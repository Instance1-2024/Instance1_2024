using UnityEngine;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.ColorManagement {
    public interface IChangeColor {
        public EColor CurrentColor { get; set; }
        
        public Collider Collider { get; set; }
        
        /// <summary>
        /// Get the Current color
        /// </summary>
        public EColor GetColor() => CurrentColor;
        
        public void ChangeColor(EColor color);
        
        /// <summary>
        /// Excludes the layer from the collider
        /// </summary>
        /// <param name="layer"> The layer to exclude</param>
        public void ExcludeLayer(LayerMask layer);
    }
}