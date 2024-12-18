using UnityEngine;

public class SCSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceWalk;
    [SerializeField] private AudioSource _audioSourceJump;
    [SerializeField] private AudioClip _clipLand;
    [SerializeField] private AudioClip _clipJump;
    //[SerializeField] private AudioClip _walkClip;
    //[SerializeField] private AudioClip _jumpUpClip; // when player leaves the floor
    //[SerializeField] private AudioClip _jumpDownClip; // when the player hit the floor post jump

    private void Awake()
    {
    }

    public void PlayJumpUpSound()
    {
        _audioSourceJump.clip = _clipJump;
        _audioSourceJump.Play();
    }

    public void PlayJumpDownSound()
    {
        _audioSourceJump.clip = _clipLand;
        _audioSourceJump.Play();
    }

    public void PlayWalkSound()
    {
        _audioSourceWalk.Play();
    }
}
