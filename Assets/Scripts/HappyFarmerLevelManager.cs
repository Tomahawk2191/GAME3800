using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HappyFarmerLevelManager : MonoBehaviour
{
    public float realTimer;
    public float displayTimer;
    public TMP_Text timerText;
    public TMP_Text gameOverText;
    public TMP_Text scoreText;
    public float gameOverDuration;
    public SceneField nextScene;
    public float startingSkyValue = 115f;
    
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip happyFarmerTheme;

    [HideInInspector]
    public static float timer;

    private float colorConversionRatio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = realTimer;
        colorConversionRatio = startingSkyValue / realTimer;

        if (gameOverText)
        {
            gameOverText.text = "";
        }

        if (RenderSettings.skybox.HasProperty("_Tint"))
        {
            float skyBoxAlpha = RenderSettings.skybox.GetColor("_Tint").a;
            RenderSettings.skybox.SetColor("_Tint", new Color(startingSkyValue, startingSkyValue, startingSkyValue, skyBoxAlpha));
        }
        
        AudioSource playerAudio = player.GetComponent<AudioSource>();
        playerAudio.clip = happyFarmerTheme;
        playerAudio.loop = true;
        playerAudio.Play();
        
        if (!player)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
        DisplayTimer();
        DisplayScore();
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

    private void DisplayScore()
    {
        if (scoreText)
        {
            scoreText.text = "Score: " + CropBehavior.totalScore.ToString();
        }
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
