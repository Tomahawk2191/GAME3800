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
    private bool isGameOver = false;
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
            if (!isGameOver)
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
        isGameOver = true;
        if (gameOverText)
        {
            gameOverText.text = "Game Over\n" + "Score: " + CropBehavior.totalScore;
        }

        if (isEndingScene)
        {
            Debug.LogWarning("Starting ending sequence");
            StartCoroutine(EndingSequence());
            return;
        }
        Invoke("LoadNextScene", gameOverDuration);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator EndingSequence()
    {
        yield return new WaitForSeconds(gameOverDuration);
        
        if (gameOverText)
        {
            gameOverText.gameObject.SetActive(false);
        }
        
        if (playerAudio.isPlaying)
            playerAudio.Stop();
        playerAudio.loop = true;
        playerAudio.clip = gameOverTheme;
        playerAudio.Play();
        
        yield break;
    }
}
