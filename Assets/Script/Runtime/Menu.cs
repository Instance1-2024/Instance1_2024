using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Button Start
    public void OnStartButton ()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Start");
    }

    public void OnSettingsButton ()
    {
        Debug.Log("Settings");
    }

    public void OnQuitButton ()
    {
        //Application.Quit();
        Debug.Log("Quit");
    }
}
