using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensibility = 150f;
    public Transform playerCamera;

    private float xRotation = 0f;
    private float lookRange = 4f;
    private Vector2 mouseInput;

    private Vector3 canPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canPosition = playerCamera.localPosition;
    }

    void Update()
    {
        LookAround();
    }

    public void OnLook(InputValue data){
        mouseInput = data.Get<Vector2>();
        LookAround();
    }

    public void LookAround(){
        xRotation -= mouseInput.y * mouseSensibility * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f,0f);
        transform.Rotate(Vector3.up * mouseInput.x * mouseSensibility * Time.deltaTime);
    }

    public void OnTest() {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, lookRange)) {
            if (hit.collider.TryGetComponent<Notes>(out Notes nota)) {
                Destroy(nota.gameObject);
            }
        }
    }
}
