using UnityEngine;

namespace Script.Runtime {
    public static class SCUtils {
        
        /// <summary>
        /// Check if the other transform is the same as the self or a child of the self
        /// </summary>
        /// <param name="other">The other Transform</param>
        /// <param name="self"> The self Transform</param>
        /// <returns>True if the other transform is the same as the self or a child of the self</returns>
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
        
        /// <summary>
        /// Add the vector2 to the vector3
        /// With the x & y value of the vector3
        /// </summary>
        /// <param name="vector3">The Vector3 to add</param>
        /// <param name="vector2">The Vector2 to be added</param>
        /// <returns>The new Vector3 resulting of the addition</returns>
        public static Vector3 Vector2To3YX(Vector3 vector3, Vector2 vector2) {
            return new Vector3(vector3.x + vector2.x, vector3.y + vector2.y, vector3.z);
        }
        
        /// <summary>
        /// Calculate the position of C by rotating the vector AB
        /// </summary>
        /// <param name="posA">The position of A</param>
        /// <param name="posB">The position of B</param>
        /// <param name="angle">The angle to rotate</param>
        /// <returns>The position of C</returns>
        public static Vector2 CalculatePositionByAngle(Vector2 posA, Vector2 posB, float angle) {
            // Convert the angle from degrees to radians
            float angleRadians = angle * Mathf.Deg2Rad;

            // Calculate the length of AB (or CA since the triangle is isosceles)
            float distanceAB = Vector2.Distance(posA, posB);

            // Point C is used as a reference for the rotation
            Vector2 vectorAB = posB - posA; // Vector AB

            // Calculate the position of C by rotating the vector AB
            float cosAngle = Mathf.Cos(angleRadians);
            float sinAngle = Mathf.Sin(angleRadians);

            // Apply rotation
            Vector2 posC = new(
                cosAngle * vectorAB.x - sinAngle * vectorAB.y,
                sinAngle * vectorAB.x + cosAngle * vectorAB.y
            );

            // Normalize and put back to the correct distance
            posC = posC.normalized * distanceAB + posA;

            return posC;
        }

        
    }
}