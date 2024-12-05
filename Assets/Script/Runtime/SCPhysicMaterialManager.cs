using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SCPhysicMaterialManager : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _bounceForce;
    [SerializeField] private float _staticFrictionForce;
    [SerializeField] private float _dynamicFrictionForce;
    [SerializeField] private PhysicsMaterial _physicMaterial;
    public Rigidbody rb;

    private void Awake()
    {
        ApplyStaticFriction(_staticFrictionForce);
        ApplyDynamicFriction(_dynamicFrictionForce);
        ApplyBounce(_bounceForce);
    }

    public void ApplyStaticFriction(float friction)
    {
        friction = _staticFrictionForce;
        _physicMaterial.staticFriction = friction;
    }

    public void ApplyDynamicFriction(float friction)
    {
        friction = _dynamicFrictionForce;
        _physicMaterial.dynamicFriction = friction;
    }

    public void ApplyBounce(float bounce)
    {
        bounce = _bounceForce;
        //rb.AddForce(new Vector3(50, 0, 50));
        _physicMaterial.bounciness = bounce;
    }

}
