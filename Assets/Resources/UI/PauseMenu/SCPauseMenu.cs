using Script.Runtime.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SCPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseGO;
    [SerializeField] private GameObject _settingsGO;
    [SerializeField] private SCInputManager _inputManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputManager.OnPauseEvent.Performed.AddListener(OpenPause);
        Debug.Log("Halojrpojpreg");
    }

    private void OpenPause()
    {
        Debug.Log("Halojrpojpreg");
        _pauseGO.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void PlayButton()
    {
        _pauseGO.SetActive(!_pauseGO.activeSelf);
        Time.timeScale = 1.0f;
    }

    public void SettingsButton()
    {
        _settingsGO.SetActive(!_settingsGO.activeSelf);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Alexis_UI");
    }
}
