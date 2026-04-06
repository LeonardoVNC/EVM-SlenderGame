using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public InputActionReference sprintAction;

    private Vector2 movementInput;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    public void OnMovement(InputValue data) {
        movementInput = data.Get<Vector2>();
    }

    public void MovePlayer () {
        Vector3 direction = transform.right * movementInput.x + transform.forward * movementInput.y;
        if (sprintAction.action.IsPressed()){
            movementSpeed = 7.5f;
        } else {
            movementSpeed = 5f;
        }
        rb.linearVelocity = new Vector3(direction.x * movementSpeed, rb.linearVelocity.y, direction.z * movementSpeed);
    }
}
