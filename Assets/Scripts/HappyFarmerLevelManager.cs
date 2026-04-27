using System.Collections;
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

    [SerializeField] private bool isEndingScene = false;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip happyFarmerTheme;
    [SerializeField] private AudioClip gameOverTheme; 
    private AudioSource playerAudio;
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
        
        playerAudio = player.GetComponent<AudioSource>();
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
        if (gameOverText)
        {
            gameOverText.text = "Game Over\n" + "Score: " + CropBehavior.totalScore;
        }

        if (isEndingScene)
        {
            StartCoroutine(EndingSequence());
        }
        Invoke("LoadNextScene", gameOverDuration);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator EndingSequence()
    {
        Debug.LogWarning("Starting ending sequence");
        yield return new WaitForSeconds(gameOverDuration);
        if (gameOverText)
        {
            gameOverText.gameObject.SetActive(false);
        }
        
        playerAudio.Stop();
        playerAudio.clip = gameOverTheme;
        yield return null;
    }
}
