using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcadeCabinetBehavior : MonoBehaviour, IPointerDownHandler
{
    public SceneField nextScene;

    public RawImage arcadeCabinetScreen;
    public Texture2D initialScreen;
    public Texture2D nextScreen;

    private bool screenTransitioned;
    private bool coinInserted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetScreen();
        BeginAsyncLoad();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScreen();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CoinBehavior.hasCoin)
        {
            coinInserted = true;
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
}
