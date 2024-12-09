using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SCOptionPanel : MonoBehaviour
{

    public Toggle FullscreenTog ;
    public List<ResItem> Resolutions = new List<ResItem>();
    private int _selectedResolution;   
    public Text ResolutionLabel;
    public AudioMixer TheMixer;
    public Text MasterLabel, MusicLabel, SFXLabel;
    public Slider MasterSlider, MusicSlider, SFXSlider;

    void Start()
    {
        FullscreenTog.isOn = Screen.fullScreen;
        _selectedResolution = 2;
        UpdateResLabel();

        float vol = 0f; // needed to store the GetFloat
        TheMixer.GetFloat("MasterVol", out vol);
        MasterSlider.value = vol;
        TheMixer.GetFloat("MusicVol", out vol);
        MusicSlider.value = vol;
        TheMixer.GetFloat("SFXVol", out vol);
        SFXSlider.value = vol;


        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    void Update()
    {
        
    }

    public void ResLeft()
    {
        _selectedResolution++;

        if (_selectedResolution > Resolutions.Count - 1)
            _selectedResolution = Resolutions.Count - 1;

        UpdateResLabel();
    }

    public void ResRight()
    {
        _selectedResolution--;

        if (_selectedResolution < 0)
            _selectedResolution = 0;

        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        ResolutionLabel.text = Resolutions[_selectedResolution].Horizontal.ToString() + " X " + Resolutions[_selectedResolution].Vertical.ToString();
    }
    public void ApplyGraphics()
    {
        Screen.SetResolution( Resolutions[_selectedResolution].Horizontal, Resolutions[_selectedResolution].Vertical, FullscreenTog.isOn);
    }


    public void SetMasterVolume()
    {   // +80 pour le ramener entre 0 et 100
        MasterLabel.text = (MasterSlider.value + 80).ToString() + " %";
        TheMixer.SetFloat("MasterVol", MasterSlider.value);
        PlayerPrefs.SetFloat("MasterVol", MasterSlider.value);
    }
    public void SetMusicVolume()
    {   // +80 pour le ramener entre 0 et 100
        MusicLabel.text = (MusicSlider.value + 80).ToString() + " %";
        TheMixer.SetFloat("MusicVol", MusicSlider.value);
        PlayerPrefs.SetFloat("MusicVol", MusicSlider.value);
    }
    public void SetSFXVolume()
    {   // +80 pour le ramener entre 0 et 100
        SFXLabel.text = (SFXSlider.value + 80).ToString() + " %";
        TheMixer.SetFloat("SFXVol", SFXSlider.value);
        PlayerPrefs.SetFloat("SFXVol", SFXSlider.value);
    }

    public void ResetAudio()
    {
        MasterSlider.value = MasterSlider.minValue + 50;
        MusicSlider.value = MusicSlider.minValue + 50;
        SFXSlider.value = SFXSlider.minValue + 50;
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}




[System.Serializable]
public class ResItem
{
    public int Horizontal, Vertical;
}
