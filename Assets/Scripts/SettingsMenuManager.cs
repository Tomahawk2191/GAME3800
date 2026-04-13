using System;
using UnityEngine;
using UnityEngine.InputSystem;
<<<<<<< HEAD
using UnityEngine.SceneManagement;
=======
>>>>>>> 9448574 (feat: settings menu ui (#33))
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Settings Menu")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button exitButton;
    
    [Header("Input Action")]
    [SerializeField] private InputActionReference ToggleMenu;
    
    [Header("Player Look")]
<<<<<<< HEAD
<<<<<<< HEAD
    [SerializeField] private GameObject player;
=======
>>>>>>> 9448574 (feat: settings menu ui (#33))
=======
    [SerializeField] private GameObject player;
>>>>>>> 8a82176 (feat: settings menu, background audio (#34))
    [SerializeField] private PlayerLook playerLook;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    public Action OnMenuToggle;
    
    void Start()
    {
<<<<<<< HEAD
<<<<<<< HEAD
        volumeSlider.onValueChanged.AddListener(value => audioSource.volume = value);
        exitButton.onClick.AddListener(ExitGame);
        
        player = GameObject.FindGameObjectWithTag("Player");
        if (!playerLook)
            playerLook = player.GetComponent<PlayerLook>();
        if (!audioSource)
            audioSource = player.GetComponent<AudioSource>();
        settingsMenu.SetActive(false);
=======
        volumeSlider.value = AudioListener.volume;
=======
        volumeSlider.onValueChanged.AddListener(value => audioSource.volume = value);
>>>>>>> 8a82176 (feat: settings menu, background audio (#34))
        exitButton.onClick.AddListener(ExitGame);
        
        player = GameObject.FindGameObjectWithTag("Player");
        if (!playerLook)
<<<<<<< HEAD
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player)
                playerLook = player.GetComponent<PlayerLook>();
        }
>>>>>>> 9448574 (feat: settings menu ui (#33))
=======
            playerLook = player.GetComponent<PlayerLook>();
        if (!audioSource)
            audioSource = player.GetComponent<AudioSource>();
>>>>>>> 8a82176 (feat: settings menu, background audio (#34))
    }

    void OnEnable()  { ToggleMenu.action.performed += OnCancel; }
    void OnDisable() { ToggleMenu.action.performed -= OnCancel; }

<<<<<<< HEAD
<<<<<<< HEAD
    // Updates when cancel button (ESC) is pressed
=======
>>>>>>> 9448574 (feat: settings menu ui (#33))
=======
    // Updates when cancel button (ESC) is pressed
>>>>>>> 8a82176 (feat: settings menu, background audio (#34))
    void OnCancel(InputAction.CallbackContext ctx)
    {
        bool newState = !settingsMenu.activeInHierarchy;
        settingsMenu.SetActive(newState);
        Time.timeScale = settingsMenu.activeInHierarchy ? 0 : 1;
        OnMenuToggle?.Invoke();
    }
<<<<<<< HEAD
<<<<<<< HEAD

=======
    
>>>>>>> 9448574 (feat: settings menu ui (#33))
=======

>>>>>>> 8a82176 (feat: settings menu, background audio (#34))
    void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
