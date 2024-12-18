using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SCMusic : MonoBehaviour
{

    [SerializeField]private AudioClip _music;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _music;
        _audioSource.loop = true;
        _audioSource.Play();
    }

}
