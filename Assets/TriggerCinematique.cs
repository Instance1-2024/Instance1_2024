using UnityEngine;

public class TriggerCinematique : MonoBehaviour
{
    public Animation animationCinematique;
    public bool destroyTrigger;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {

            animationCinematique.Play(animationCinematique.clip.name);
            destroyTrigger = true;

        }
    }
    private void OnTriggerStay()
    {
        if (destroyTrigger && !animationCinematique.isPlaying)
            Destroy(gameObject);
    }

}
