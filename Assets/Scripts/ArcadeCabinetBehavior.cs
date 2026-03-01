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

    public AudioClip bootUpSFX;
    public AudioClip insertSFX;

    private bool screenTransitioned;
    private Ray playerDetection;
    private bool coinInserted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDetection = new Ray(transform.position, transform.forward);
        ResetScreen();
        StartCoroutine(BeginAsyncLoad());
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
            AudioSource.PlayClipAtPoint(bootUpSFX, Camera.main.transform.position);
        }
    }

    private void CheckPlayer()
    {
        if(coinInserted == false && player.Raycast(playerDetection, out RaycastHit hitInfo, maxDistance) && Mouse.current.leftButton.wasPressedThisFrame) 
        {
            coinInserted = true;
            AudioSource.PlayClipAtPoint(insertSFX, Camera.main.transform.position);
            Invoke(nameof(TransitionScene), insertSFX.length);
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
    }

    private void TransitionScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
