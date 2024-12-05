using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation))]
public class SCPlatformInteraction : MonoBehaviour
{
    Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pebble"))
        {
            _animation.Stop();
            _animation.Play("ShowPlatform");
            Debug.Log("hit");
        }
    }
}
