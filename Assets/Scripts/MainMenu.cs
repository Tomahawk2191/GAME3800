using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("ArcadeGraybox");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}