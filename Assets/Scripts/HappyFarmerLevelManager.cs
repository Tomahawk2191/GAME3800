using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HappyFarmerLevelManager : MonoBehaviour
{
    public float realTimer;
    public float displayTimer;
    public TMP_Text timerText;
    public TMP_Text gameOverText;
    public float gameOverDuration;
    public SceneField nextScene;

    [HideInInspector]
    public static float timer;

    private float colorConversionRatio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = realTimer;

        if (gameOverText)
        {
            gameOverText.text = "";
        }

        if (RenderSettings.skybox.HasProperty("_Tint"))
        {
            colorConversionRatio = RenderSettings.skybox.GetColor("_Tint").r / realTimer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
        DisplayTimer();
    }

    private void CountDown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            GameOver();
        }
        if (RenderSettings.skybox.HasProperty("_Tint"))
        {
            Color nextColor = RenderSettings.skybox.GetColor("_Tint") - new Color(colorConversionRatio * Time.deltaTime, colorConversionRatio * Time.deltaTime, colorConversionRatio * Time.deltaTime, 0);

            RenderSettings.skybox.SetColor("_Tint", nextColor);
        }
    }

    private void DisplayTimer()
    {
        timerText.text = (timer * displayTimer / realTimer).ToString("0.00");
    }

    private void GameOver()
    {
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        player.Disable();

        if (gameOverText)
        {
            gameOverText.text = "Game Over\n" + "Score: " + CropBehavior.totalScore;
        }

        Invoke("LoadNextScene", gameOverDuration);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
