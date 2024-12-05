using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation))]
public class SCPlatformInteraction : MonoBehaviour
{
    [SerializeField] private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pebble"))
        {
            //Stop animation if one was started on collision
            _animation.Stop();

            //Start the animation of the platform apparition on collision
            _animation.Play("ShowPlatform");
            Debug.Log("hit");
        }
    }
}
