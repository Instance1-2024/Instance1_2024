using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class SCPlayCinematique : MonoBehaviour
{
    [SerializeField] private VideoClip _videoClip;
    private VideoPlayer _introVideoPlayer;
    [SerializeField] private string _sceneName;
    private bool _canStartChecking = false;
    [SerializeField] private int _timeEndOfVideo;
    private void Awake()
    {
        _introVideoPlayer = GetComponent<VideoPlayer>();  
    }

    void Start()
    {
        _introVideoPlayer.clip =_videoClip;
        _introVideoPlayer.Play();
        
    }
    void Update()
    {
        
        if (  _introVideoPlayer.time >= _timeEndOfVideo)
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(_sceneName);
    }

}
