using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Settings Menu")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button exitButton;
    [SerializeField] private SceneField mainMenu;
    
    [Header("Input Action")]
    [SerializeField] private InputActionReference toggleMenu;
    
    [Header("Player Look")]
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerLook playerLook;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    

    public Action OnMenuToggle;
    
    void Start()
    {
        Time.timeScale = 1f;
        settingsMenu.SetActive(false);
        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(value => audioSource.volume = value);
        exitButton.onClick.AddListener(ExitGame);
        
        if (!playerLook)
        {
            if (player)
                playerLook = player.GetComponent<PlayerLook>();
        }
        if (!audioSource)
            audioSource = player.GetComponent<AudioSource>();
    }

    void OnEnable()  { toggleMenu.action.performed += OnCancel; }
    void OnDisable() { toggleMenu.action.performed -= OnCancel; }
    void OnDestroy() { toggleMenu.action.performed -= OnCancel; }
    
    // Updates when the cancel button (ESC) is pressed
    void OnCancel(InputAction.CallbackContext ctx)
    {
        bool newState = !settingsMenu.activeInHierarchy;
        settingsMenu.SetActive(newState);
        Time.timeScale = settingsMenu.activeInHierarchy ? 0 : 1;
        OnMenuToggle?.Invoke();
    }

    void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
