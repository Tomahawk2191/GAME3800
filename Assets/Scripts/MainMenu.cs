using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneField nextScene;
    [SerializeField] private GameObject settingsMenu;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    
    public void ToggleSettingsMenu()
    {
        settingsMenu.SetActive(!settingsMenu.activeInHierarchy);
    }
}