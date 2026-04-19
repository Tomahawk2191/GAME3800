using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public Transform playerCamera;
    public Vector2 mouseSensitivity;
    public InputActionReference lookActionReference;
    private Vector2 _rotationXY;
    private SettingsMenuManager _settingsMenuManager;
    private bool IsLookEnabled { get; set; }
    
    void Start()
    {
        if (!lookActionReference)
        {
            Debug.LogError("Look Action Reference is not set! Use Player/Look for the player model.");
        }

        IsLookEnabled = true;
        _settingsMenuManager = FindFirstObjectByType<SettingsMenuManager>();
        if (_settingsMenuManager)
            _settingsMenuManager.OnMenuToggle += OnMenuToggle;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnMenuToggle()
    {
        IsLookEnabled = !IsLookEnabled;
        Cursor.lockState = IsLookEnabled ? CursorLockMode.Locked : CursorLockMode.Confined;
        Cursor.visible = !IsLookEnabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerCamera)
        {
            return;
        }

        if (IsLookEnabled)
        {
            var playerLook = lookActionReference.action.ReadValue<Vector2>();

            _rotationXY.x -= playerLook.y * mouseSensitivity.y;
            _rotationXY.y += playerLook.x * mouseSensitivity.x;

            _rotationXY.x = Mathf.Clamp(_rotationXY.x, -90f, 90f);

            transform.eulerAngles = new Vector3(0f, _rotationXY.y, 0f);
            playerCamera.localEulerAngles = new Vector3(_rotationXY.x, 0f, 0f);
        }
    }
}
