using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SCOptionPanel : MonoBehaviour
{

    public Toggle FullscreenTog ;
    public List<ResItem> Resolutions = new List<ResItem>();
    private int _selectedResolution;   
    public Text ResolutionLabel;

    void Start()
    {
        FullscreenTog.isOn = Screen.fullScreen;
        _selectedResolution = 2;
        UpdateResLabel();
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
        //Screen.fullScreen = FullscreenTog.isOn;

        Screen.SetResolution( Resolutions[_selectedResolution].Horizontal, Resolutions[_selectedResolution].Vertical, FullscreenTog.isOn);
    }

}

[System.Serializable]
public class ResItem
{
    public int Horizontal, Vertical;
}
