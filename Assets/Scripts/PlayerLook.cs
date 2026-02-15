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
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerCamera)
        {
            return;
        }

        Vector2 mouseAxis = LookActionReference.action.ReadValue<Vector2>();
        
        var mouseInput = new Vector2(mouseAxis.x, mouseAxis.y);

        rotationXY.x -= mouseInput.y * MouseSensitivity.y;
        rotationXY.y += mouseInput.x * MouseSensitivity.x;

        rotationXY.x = Mathf.Clamp(rotationXY.x, -90f, 90f);

        transform.eulerAngles = new Vector3(0f, rotationXY.y, 0f);
        PlayerCamera.localEulerAngles = new Vector3(rotationXY.x, 0f, 0f);
    }
}
