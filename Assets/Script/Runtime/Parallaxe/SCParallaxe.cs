using UnityEngine;

public class SCParallaxe : MonoBehaviour
{
    private float _length, _startPos;

    [SerializeField] private Transform _camTransform;

    [SerializeField] private float _parallaxEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = (_camTransform.position.x * _parallaxEffect);
        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);
    }
}
