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
        
        public static Vector3 Vector2To3Y(Vector3 vector3, Vector2 vector2) {
            return new Vector3(vector3.x + vector2.x, vector3.y + vector2.y, vector3.z);
        }
    }
}