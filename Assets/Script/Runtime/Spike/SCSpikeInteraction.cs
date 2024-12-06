using UnityEngine;

public class SCSpikeInteraction : MonoBehaviour
{
    //[SerializeField] GameObject _myPlayer;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Death");
        }
    }
}
