using UnityEngine;
using UnityEngine.InputSystem;

public class ViewBobController : MonoBehaviour
{
    [SerializeField]
    private float amplitude;
    [SerializeField] 
    private float frequency;
    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private Transform camHolder;

    public InputActionReference MoveActionReference;

    private float toggleSpeed = 0.1f;
    private Vector3 startPos;
    private CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        startPos = playerCamera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMotion();
        ResetPosition();
        playerCamera.LookAt(FocusTarget());
    }

    private void PlayMotion(Vector3 motion)
    {
        playerCamera.localPosition += motion;
    }

    private void CheckMotion()
    {
        var moveAction = MoveActionReference.action.ReadValue<Vector2>();
        float speed = moveAction.magnitude;

        if (speed < toggleSpeed || !characterController.isGrounded)
        {
            return;
        }

        PlayMotion(FootStepMotion());
    }

    private void ResetPosition()
    {
        if(playerCamera.localPosition == startPos)
        {
            return;
        } else
        {
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, startPos, 1 * Time.deltaTime);
        }
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x = Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + camHolder.localPosition.y, transform.position.z);
        pos += camHolder.forward * 15.0f;
        return pos;
    }
}
