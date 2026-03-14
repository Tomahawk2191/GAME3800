using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcadeCabinetBehavior : MonoBehaviour
{
    public SceneField nextScene;
    public Collider player;
    public float maxDistance = 5f;

    public RawImage arcadeCabinetScreen;
    public Texture2D initialScreen;
    public Texture2D nextScreen;

    private bool screenTransitioned;
    private Ray playerDetection;
    private bool coinInserted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDetection = new Ray(transform.position, transform.forward);
        ResetScreen();
        BeginAsyncLoad();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScreen();

        if(CoinBehavior.hasCoin)
        {
            CheckPlayer();
        }
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

    private void CheckPlayer()
    {
        if(player.Raycast(playerDetection, out RaycastHit hitInfo, maxDistance) && Mouse.current.leftButton.wasPressedThisFrame) 
        {
            Debug.Log("Raycast has hit");
            coinInserted = true;
            SceneManager.LoadScene(nextScene);
        }
    }

    private IEnumerator BeginAsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        
        operation.allowSceneActivation = false;

        while(!operation.isDone || !coinInserted)
        {
            yield return null;
        }

        operation.allowSceneActivation = true;
        Debug.Log("Scene gets loaded here");
    }
}
