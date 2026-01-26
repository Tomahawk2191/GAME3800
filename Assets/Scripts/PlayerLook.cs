using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform PlayerCamera;
    public Vector2 MouseSensitivity;

    private Vector2 rotationXY;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerCamera)
        {
            return;
        }

        Vector2 MouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        rotationXY.x -= MouseInput.y * MouseSensitivity.y;
        rotationXY.y += MouseInput.x * MouseSensitivity.x;

        rotationXY.x = Mathf.Clamp(rotationXY.x, -90f, 90f);

        transform.eulerAngles = new Vector3(0f, rotationXY.y, 0f);
        PlayerCamera.localEulerAngles = new Vector3(rotationXY.x, 0f, 0f);
    }
}
