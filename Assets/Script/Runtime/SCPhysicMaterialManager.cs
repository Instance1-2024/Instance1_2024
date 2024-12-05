using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SCPhysicMaterialManager : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _bounceForce;
    [SerializeField] private float _staticFrictionForce;
    [SerializeField] private float _dynamicFrictionForce;

    [SerializeField] private PhysicsMaterial _physicMaterial;

    private void Awake()
    {
        //Call Static and Dynamic Friction and Bounciness Application Function in the awake
        ApplyStaticFriction(_staticFrictionForce);
        ApplyDynamicFriction(_dynamicFrictionForce);
        ApplyBounce(_bounceForce);
    }

    public void ApplyStaticFriction(float friction)
    {
        //Apply the value set in the inspector to the Static Friction in the Physic Material
        friction = _staticFrictionForce;
        _physicMaterial.staticFriction = friction;
    }

    public void ApplyDynamicFriction(float friction)
    {
        //Apply the value set in the inspector to the Static Friction in the Physic Material
        friction = _dynamicFrictionForce;
        _physicMaterial.dynamicFriction = friction;
    }

    public void ApplyBounce(float bounce)
    {
        //Apply the value set in the inspector to the Static Friction in the Physic Material
        bounce = _bounceForce;
        _physicMaterial.bounciness = bounce;
    }

}
