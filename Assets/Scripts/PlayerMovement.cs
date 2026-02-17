using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSmoothTime;
    public float MoveSpeed;
    public InputActionReference MoveActionReference;

    private CharacterController characterController;
    private Vector3 currentMoveVelocity;
    private Vector3 moveDampVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!MoveActionReference)
        {
           Debug.LogError("Move Action Reference is not set! Use Player/Move for the player model."); 
        }
        
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 axisVector = MoveActionReference.action.ReadValue<Vector2>(); 
        
        Vector3 playerInput = new Vector3(
            axisVector.x,
            0f, 
            axisVector.y);

        if (playerInput.magnitude > 1f)
        {
            playerInput.Normalize();
        }

        Vector3 moveVector = transform.TransformDirection(playerInput);

        currentMoveVelocity = Vector3.SmoothDamp(
            currentMoveVelocity,
            moveVector * MoveSpeed,
            ref moveDampVelocity,
            MoveSmoothTime);

        characterController.Move(currentMoveVelocity * Time.deltaTime);
    }
}
