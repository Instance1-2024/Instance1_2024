using UnityEngine;
using UnityEngine.Serialization;

public class TriggerCinematic : MonoBehaviour {
    public Animation animationCinematic;
    public bool destroyTrigger;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {

            animationCinematic.Play(animationCinematic.clip.name);
            destroyTrigger = true;

        }
    }
    private void OnTriggerStay()
    {
        if (destroyTrigger && !animationCinematic.isPlaying)
            Destroy(gameObject);
    }

}
