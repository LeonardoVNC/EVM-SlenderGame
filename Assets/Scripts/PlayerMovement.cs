using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpSpeed = 5f;
    public InputActionReference sprintAction;
    public InputActionReference jumpAction;

    private Vector2 movementInput;
    private Rigidbody rb;

    private AudioSource audioSource;

    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        MovePlayer();
        isJumping = rb.linearVelocity.y >= 0.1f;
    }

    public void OnMovement(InputValue data) {
        movementInput = data.Get<Vector2>();
    }

    public void OnJump(InputValue data) {
        if(jumpAction.action.IsPressed() && !isJumping) {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpSpeed, rb.linearVelocity.z);
        }
    }

    public void MovePlayer () {
        Vector3 direction = transform.right * movementInput.x + transform.forward * movementInput.y;
        if (sprintAction.action.IsPressed()){
            movementSpeed = 7.5f;
        } else {
            movementSpeed = 5f;
        }
        rb.linearVelocity = new Vector3(direction.x * movementSpeed, rb.linearVelocity.y, direction.z * movementSpeed);

        if (!isJumping && movementInput.magnitude > 0.1f && !audioSource.isPlaying) {
            audioSource.Play();
        } else if (isJumping || movementInput.magnitude <= 0.1f && audioSource.isPlaying) {
            audioSource.Pause();
        }
    }
}
