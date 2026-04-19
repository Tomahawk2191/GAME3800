using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneField nextScene;
    public void PlayGame()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}