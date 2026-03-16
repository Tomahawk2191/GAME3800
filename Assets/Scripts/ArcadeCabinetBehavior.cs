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

    public GameObject cinemachineVirtualCamera;
    public float targetWidth = 0.75f;

    private bool screenTransitioned;
    private Ray playerDetection;
    private bool coinInserted = false;
    private Camera _camera;

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

            if(bootUpSFX)
            {
                AudioSource.PlayClipAtPoint(bootUpSFX, Camera.main.transform.position);
                bootUpSFX = null;
            }
        }
    }

    private void CheckPlayer()
    {
        if(coinInserted == false && player.Raycast(playerDetection, out RaycastHit hitInfo, maxDistance) && Mouse.current.leftButton.wasPressedThisFrame) 
        {
            coinInserted = true;

            if(insertSFX)
            {
                AudioSource.PlayClipAtPoint(insertSFX, Camera.main.transform.position);
            }

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
        cinemachineVirtualCamera.SetActive(true);
        StartCoroutine(nameof(PillarBox));
    }

    IEnumerator PillarBox()
    {
        _camera = Camera.main.GetComponent<Camera>();
        Rect rect = _camera.rect;
        while (rect.width != targetWidth)
        {
            rect.width -= Time.deltaTime;
            if(rect.width < targetWidth)
            {
                rect.width = targetWidth;
            }

            rect.x = (1f - rect.width) / 2f;
            _camera.rect = rect;
            yield return new WaitForSecondsRealtime(Time.deltaTime);  
        }
        LoadNextScene();
        yield break;
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
