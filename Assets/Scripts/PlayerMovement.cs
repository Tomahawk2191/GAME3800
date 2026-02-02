using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSmoothTime;
    public float MoveSpeed;

    private CharacterController characterController;
    private Vector3 currentMoveVelocity;
    private Vector3 moveDampVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerInput = new Vector3(
            Input.GetAxisRaw("Horizontal"), 
            0f, 
            Input.GetAxisRaw("Vertical"));

        if (PlayerInput.magnitude > 1f)
        {
            PlayerInput.Normalize();
        }

        Vector3 MoveVector = transform.TransformDirection(PlayerInput);

        currentMoveVelocity = Vector3.SmoothDamp(
            currentMoveVelocity,
            MoveVector * MoveSpeed,
            ref moveDampVelocity,
            MoveSmoothTime);

        characterController.Move(currentMoveVelocity * Time.deltaTime);
    }
}
