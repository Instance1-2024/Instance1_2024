using UnityEngine;

public class SCDeadlyObstacleInteraction : MonoBehaviour
{
    /// <summary>
    /// Death of player on collision with Spike
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Death");

        }
    }
}
