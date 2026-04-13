using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensibility = 150f;
    public Transform playerCamera;

    private float xRotation = 0f;
    private float collectRange = 4f;
    private Vector2 mouseInput;

    private Vector3 canPosition;
    private bool isActive = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canPosition = playerCamera.localPosition;
    }

    void Update()
    {
        if (isActive) {
            LookAround();
        }
    }
    
    public void setActive (bool active) {
        isActive = active;
    }

    public void OnLook(InputValue data){
        if(isActive) {
            mouseInput = data.Get<Vector2>();
            LookAround();
        }
    }

    public void LookAround(){
        xRotation -= mouseInput.y * mouseSensibility * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f,0f);
        transform.Rotate(Vector3.up * mouseInput.x * mouseSensibility * Time.deltaTime);
    }

    public void OnCollect() {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, collectRange)) {
            if (hit.collider.TryGetComponent<Notes>(out Notes nota)) {
                Destroy(nota.gameObject);
                GameManager.Instance.AddNote();
            }
        }
    }
}
