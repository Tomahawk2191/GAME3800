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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = realTimer;

        if (gameOverText)
        {
            gameOverText.text = "";
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
    }

    private void DisplayTimer()
    {
        timerText.text = (timer * displayTimer / realTimer).ToString("0.00");
    }

    private void GameOver()
    {
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
