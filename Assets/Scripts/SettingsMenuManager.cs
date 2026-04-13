using System;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField] private PlayerLook playerLook;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    public Action OnMenuToggle;
    
    void Start()
    {
        volumeSlider.value = AudioListener.volume;
        exitButton.onClick.AddListener(ExitGame);

        if (!playerLook)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player)
                playerLook = player.GetComponent<PlayerLook>();
        }
    }

    void OnEnable()  { ToggleMenu.action.performed += OnCancel; }
    void OnDisable() { ToggleMenu.action.performed -= OnCancel; }

    void OnCancel(InputAction.CallbackContext ctx)
    {
        bool newState = !settingsMenu.activeInHierarchy;
        settingsMenu.SetActive(newState);
        Time.timeScale = settingsMenu.activeInHierarchy ? 0 : 1;
        OnMenuToggle?.Invoke();
    }
    
    void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
