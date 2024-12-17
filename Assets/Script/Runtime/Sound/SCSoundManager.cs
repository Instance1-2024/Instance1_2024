using UnityEngine;

public class SCSoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _walkClip;
    [SerializeField] private AudioClip _jumpUpClip; // when player leaves the floor
    [SerializeField] private AudioClip _jumpDownClip; // when the player hit the floor post jump

    private void Awake()
    {
        _audioSource = transform.parent.parent.GetComponent<AudioSource>();
    }

    public void PlayJumpUpSound()
    {
        _audioSource.clip = _jumpUpClip;
        _audioSource.Play();
    }

    public void PlayJumpDownSound()
    {
        _audioSource.clip = _jumpDownClip;
        _audioSource.Play();
    }

    public void PlayWalkSound()
    {
        _audioSource.clip = _walkClip;
        _audioSource.Play();
    }
}
