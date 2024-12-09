using UnityEngine;
using UnityEngine.Video;

namespace Script.Runtime.EndGame
{
    public class SCEndGameManager : MonoBehaviour
    {
        private VideoPlayer _endVideoPlayer;
       
        
        void Start()
        {
            _endVideoPlayer = GetComponent<VideoPlayer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //Stop the player Movement and Interaction
                
                _endVideoPlayer.Play();
            
                
            }
        }
    }
}
