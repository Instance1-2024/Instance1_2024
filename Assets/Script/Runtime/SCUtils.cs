using UnityEngine;

namespace Script.Runtime {
    public static class SCUtils {
        public static bool IsMyself(Transform other, Transform self ,GameObject child) {
            return other == self || other == child.transform || other.IsChildOf(child.transform);
        }
        
        public static bool IsMyself(Transform other, Transform self) {
            return other == self || other.IsChildOf(self.transform);
        }
        
        /// <summary>
        /// Check if the layer is in the mask
        /// </summary>
        /// <param name="layer"> The layer to check</param>
        /// <param name="mask">The layer mask to check</param>
        /// <returns>True if the layer is in the mask</returns>
        public static bool CompareLayerMask( int layer, LayerMask mask) => (mask.value & (1 << layer)) != 0;
    }
}