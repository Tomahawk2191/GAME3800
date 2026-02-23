using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public Transform PlayerCamera;
    public Vector2 MouseSensitivity;
    public InputActionReference LookActionReference;
    private Vector2 rotationXY;

    void Start()
    {
        if (!LookActionReference)
        {
            Debug.LogError("Look Action Reference is not set! Use Player/Look for the player model.");
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerCamera)
        {
            return;
        }

        var playerLook = LookActionReference.action.ReadValue<Vector2>();

        rotationXY.x -= playerLook.y * MouseSensitivity.y;
        rotationXY.y += playerLook.x * MouseSensitivity.x;

        rotationXY.x = Mathf.Clamp(rotationXY.x, -90f, 90f);

        transform.eulerAngles = new Vector3(0f, rotationXY.y, 0f);
        PlayerCamera.localEulerAngles = new Vector3(rotationXY.x, 0f, 0f);
    }
}
