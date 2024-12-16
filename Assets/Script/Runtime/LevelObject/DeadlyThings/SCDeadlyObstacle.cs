using Script.Runtime.Player;
using UnityEngine;

namespace Script.Runtime.LevelObject.DeadlyThings {
    public class SCDeadlyObstacle : MonoBehaviour {
        /// <summary>
        /// Death of player on collision with Spike
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Death");
                other.gameObject.GetComponent<SCPlayerRespawnAtCheckpoint>().OnRespawn();

            }

            if (other.gameObject.CompareTag("Pebble"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
