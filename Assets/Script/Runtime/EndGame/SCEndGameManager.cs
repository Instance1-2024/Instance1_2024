using UnityEngine;
using UnityEngine.Video;

namespace Script.Runtime.EndGame
{
    public class SCEndGameManager : MonoBehaviour
    {
        [SerializeField] private VideoClip _videoClipFullMemory;
        [SerializeField] private VideoClip _videoClip;
        private VideoPlayer _endVideoPlayer;
        private void Awake()
        {
            _endVideoPlayer = GetComponent<VideoPlayer>();  
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //Stop the player Movement and Interaction

                if (SCProphecyManager.Instance == null)
                {
                    Debug.Log("Playing Incomplete End Scene Cinematic !");
                    _endVideoPlayer.clip = _videoClip;
                    _endVideoPlayer.Play(); // if we dont have the prophecy manager we consider we have an incomplete end ( so no matter what the game has an end)
                    return;
                }

                if (SCProphecyManager.Instance.GetMemoryIds().Count == 7)
                {
                    Debug.Log("Playing Incomplete End Scene Cinematic !");
                    _endVideoPlayer.clip = _videoClipFullMemory;
                    _endVideoPlayer.Play();
                }
                else
                {
                    Debug.Log("Playing Incomplete End Scene Cinematic !");
                    _endVideoPlayer.clip = _videoClip;
                    _endVideoPlayer.Play();
                }


            }
        }
    }
}
