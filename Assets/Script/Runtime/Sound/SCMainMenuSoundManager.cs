using System.Collections;
using UnityEngine;

public class SCMainMenuSoundManager : MonoBehaviour
{
    public SCSceneChanger SceneChanger;

    public AudioSource AudioSourceUI;
    public AudioClip _exitClip;
    public AudioClip _settingsClip;
    public AudioClip _creditsClip;
    public AudioClip _moveClip;
    public void PlayExitSound()
    {
        if (!AudioSourceUI) return;
        AudioSourceUI.clip = _exitClip;
        AudioSourceUI.Play();
        // add delay to hear sound
        SceneChanger.ExitGame();
    }

    public void PlaySettingsSound()
    {
        if (!AudioSourceUI) return;
        AudioSourceUI.clip = _settingsClip;
        AudioSourceUI.Play();
    }
    public void PlayCreditsSound()
    {
        if (!AudioSourceUI) return;
        AudioSourceUI.clip = _creditsClip;
        AudioSourceUI.Play();
    }
    public void PlayMoveSound()
    {
        if (!AudioSourceUI) return;
        AudioSourceUI.clip = _moveClip;
        AudioSourceUI.Play();
    }
}
