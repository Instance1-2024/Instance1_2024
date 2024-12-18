using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SCButtonSound : MonoBehaviour
{
    [SerializeField]private AudioClip _music;
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _music;
    }
    public void OnClickButton()
    {
        _audioSource.Stop();
        _audioSource.Play();
    }
}
