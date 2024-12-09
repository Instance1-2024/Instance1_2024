using UnityEngine;

namespace Script.Runtime {


    [RequireComponent(typeof(Rigidbody))]
    public class SCPhysicMaterialManager : MonoBehaviour {
        [SerializeField, Range(0, 1)] private float _bounceForce;
        [SerializeField] private float _staticFrictionForce;
        [SerializeField] private float _dynamicFrictionForce;

        [SerializeField] private PhysicsMaterial _physicMaterial;

        /// <summary>
        /// //Call Static and Dynamic Friction and Bounciness Application Function in the awake
        /// </summary>
        private void Awake() {
            ApplyStaticFriction(_staticFrictionForce);
            ApplyDynamicFriction(_dynamicFrictionForce);
            ApplyBounce(_bounceForce);
        }

        /// <summary>
        /// Apply the value to the Static Friction in the Physic Material
        /// </summary>
        /// <param name="friction"></param>
        public void ApplyStaticFriction(float friction) {
            _staticFrictionForce = friction;
            _physicMaterial.staticFriction = friction;
        }

        /// <summary>
        /// Apply the value to the Dynamic Friction in the Physic Material
        /// </summary>
        /// <param name="friction"></param>
        public void ApplyDynamicFriction(float friction) {
            _dynamicFrictionForce = friction;
            _physicMaterial.dynamicFriction = friction;
        }
        
        /// <summary>
        /// Apply the value to the Static Friction and the Dynamic Friction in the Physic Material
        /// </summary>
        /// <param name="friction"></param>
        public void ApplyFrictions(float friction) {
            ApplyStaticFriction(friction);
            ApplyDynamicFriction(friction);
        }

        /// <summary>
        /// Apply the value to the Bounce in the Physic Material
        /// </summary>
        /// <param name="bounce"></param>
        public void ApplyBounce(float bounce) {
            _bounceForce = bounce ;
            _physicMaterial.bounciness = bounce;
        }
        
        /// <summary>
        /// Apply the mode to the Friction Combine in the Physic Material
        /// </summary>
        /// <param name="mode"></param>
        public void ApplyFrictionCombine(PhysicsMaterialCombine mode) {
            _physicMaterial.frictionCombine = mode;
        }
        
        /// <summary>
        /// Apply the mode to the Bounce Combine in the Physic Material
        /// </summary>
        /// <param name="mode"></param>
        public void ApplyBounceCombine(PhysicsMaterialCombine mode) {
            _physicMaterial.bounceCombine = mode;
        }

    }
}
