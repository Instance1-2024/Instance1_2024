using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SCPhysicMaterialManager : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _bounceForce;
    [SerializeField] private float _staticFrictionForce;
    [SerializeField] private float _dynamicFrictionForce;

    [SerializeField] private PhysicsMaterial _physicMaterial;

    /// <summary>
    /// //Call Static and Dynamic Friction and Bounciness Application Function in the awake
    /// </summary>
    private void Awake()
    {
        ApplyStaticFriction(_staticFrictionForce);
        ApplyDynamicFriction(_dynamicFrictionForce);
        ApplyBounce(_bounceForce);
    }

    /// <summary>
    /// Apply the value set in the inspector to the Static Friction in the Physic Material
    /// </summary>
    /// <param name="friction"></param>
    public void ApplyStaticFriction(float friction)
    {
        friction = _staticFrictionForce;
        _physicMaterial.staticFriction = friction;
    }

    /// <summary>
    /// Apply the value set in the inspector to the Static Friction in the Physic Material
    /// </summary>
    /// <param name="friction"></param>
    public void ApplyDynamicFriction(float friction)
    {
        friction = _dynamicFrictionForce;
        _physicMaterial.dynamicFriction = friction;
    }

    /// <summary>
    /// Apply the value set in the inspector to the Static Friction in the Physic Material
    /// </summary>
    /// <param name="bounce"></param>
    public void ApplyBounce(float bounce)
    {
        bounce = _bounceForce;
        _physicMaterial.bounciness = bounce;
    }

}
