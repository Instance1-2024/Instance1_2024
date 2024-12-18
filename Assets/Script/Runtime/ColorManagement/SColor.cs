using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Runtime.ColorManagement {
    /// <summary>
    /// The structure that contains the mesh, the material and the layer to apply to the object
    /// </summary>
    /// <param name="Material">The material to apply</param>
    /// <param name="Layer">The layer to apply</param>
    [Serializable]
    public struct SColor {
        public List<Material> Material;
        public LayerMask Layer;
    }
}