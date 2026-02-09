using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcadeCabinetBehavior : MonoBehaviour
{
    public string nextScene;

    public RawImage arcadeCabinetScreen;
    public Texture2D initialScreen;
    public Texture2D nextScreen;

    private bool screenTransitioned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetScreen();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScreen();
    }

    private void OnMouseDown()
    {
        if (CoinBehavior.hasCoin)
        {
            SceneTransition();
        }
    }

    private void SceneTransition()
    {
        // This is where the logic for the scene transition will go. Most likely cinemachine camera movement and screen changes.
        
        SceneManager.LoadScene(nextScene);
    }

    private void ResetScreen()
    {
        screenTransitioned = false;
        arcadeCabinetScreen.texture = initialScreen;
    }

    private void UpdateScreen()
    {
        if (!screenTransitioned && CoinBehavior.hasCoin)
        {
            arcadeCabinetScreen.texture = nextScreen;
        }
    }
}
