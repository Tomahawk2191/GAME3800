using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSmoothTime;
    public float MoveSpeed;
    public float gravityStrength = 9.81f;
    public InputActionReference MoveActionReference;

    private CharacterController characterController;
    private Vector3 currentMoveVelocity;
    private Vector3 moveDampVelocity;

    private Vector3 currentForceVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveAction = MoveActionReference.action.ReadValue<Vector2>();
        Vector3 PlayerInput = new Vector3(
            moveAction.x,
            0f,
            moveAction.y).normalized;

        Vector3 MoveVector = transform.TransformDirection(PlayerInput);

        currentMoveVelocity = Vector3.SmoothDamp(
            currentMoveVelocity,
            MoveVector * MoveSpeed,
            ref moveDampVelocity,
            MoveSmoothTime);

        if (!characterController.isGrounded)
        {
            currentForceVelocity.y -= gravityStrength * Time.deltaTime;
        }
        else
        {
            currentForceVelocity.y = -2f;
        }

        characterController.Move(currentForceVelocity * Time.deltaTime);
        characterController.Move(currentMoveVelocity * Time.deltaTime);
    }
}
