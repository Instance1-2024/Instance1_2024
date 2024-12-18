using UnityEngine;

public class SCSoundManager : MonoBehaviour
{
    [Header("Movements Related")]
    [SerializeField] private AudioSource _audioSourceWalk;
    [SerializeField] private AudioSource _audioSourceJump;
    [SerializeField] private AudioClip _clipLand;
    [SerializeField] private AudioClip _clipJump;

    [Header("Prophecy Related")]
    [SerializeField] private AudioSource _audioSourceProphecy;
    [SerializeField] private AudioClip _clipIncompleteProphecy;
    [SerializeField] private AudioClip _clipCompleteProphecy;

    public void AssembleProphecy()
    {
        if (SCProphecyManager.Instance)
            return;

        if (SCProphecyManager.Instance.GetMemoryIds().Count < 7)
            AssembleProphecyIncomplete();
        else
            AssembleProphecyComplete();
    }

    public void AssembleProphecyIncomplete()
    {
        if (!_audioSourceProphecy) return;

        _audioSourceProphecy.clip = _clipIncompleteProphecy;
        _audioSourceProphecy.Play();
    }
    public void AssembleProphecyComplete()
    {
        if (!_audioSourceProphecy) return;

        _audioSourceProphecy.clip = _clipCompleteProphecy;
        _audioSourceProphecy.Play();
    }

    public void PlayJumpUpSound()
    {
        if (!_audioSourceJump) return;

        _audioSourceJump.clip = _clipJump;
        _audioSourceJump.Play();
    }

    public void PlayJumpDownSound()
    {
        if (!_audioSourceJump) return;
        _audioSourceJump.clip = _clipLand;
        _audioSourceJump.Play();
    }

    public void PlayWalkSound()
    {
        if (!_audioSourceWalk) return;
        _audioSourceWalk.Play();
    }
}
