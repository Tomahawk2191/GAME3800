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
    public RectTransform leftPillarBox;
    public RectTransform rightPillarBox;
    public float pillarBoxSpeed = 20f;
    public float sceneTransitionTime = 2f;

    private bool screenTransitioned;
    private Ray playerDetection;
    private bool coinInserted = false;
    private Camera _camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDetection = new Ray(transform.position, transform.forward);
        coinInserted = false;
        ResetScreen();
        StartCoroutine(BeginAsyncLoad());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScreen();
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red);
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
        PlayerLook playerLook = FindObjectOfType<PlayerLook>();
        if (playerLook)
            playerLook.SetLookEnabled(false);
        
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement)
            playerMovement.enabled = false;
        StartCoroutine(nameof(PillarBox));
    }

    IEnumerator PillarBox()
    {
        if(leftPillarBox && rightPillarBox)
        {
            while (leftPillarBox.anchoredPosition.x < 0)
            {
                Debug.Log(leftPillarBox.anchoredPosition);
                float targetX = leftPillarBox.anchoredPosition.x + Time.deltaTime * pillarBoxSpeed;
                leftPillarBox.anchoredPosition = new Vector2(targetX, 0);
                rightPillarBox.anchoredPosition = new Vector2(-1f * targetX, 0);
                yield return new WaitForSecondsRealtime(Time.deltaTime);
            }
        }
        new WaitForSecondsRealtime(sceneTransitionTime);
        LoadNextScene();
        yield break;
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
