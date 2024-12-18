using UnityEngine;
using UnityEngine.SceneManagement;

public class SCSceneChanger : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    /// <summary>
    /// Load & Open the scene corresponding to _sceneName in SceneManager.
    /// </summary>
    private void ChangeScene()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ChangeScene();
        }
    }
}
