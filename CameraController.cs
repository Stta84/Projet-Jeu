using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; 
    public float mouseSensitivity = 200f; 
    public float heightOffset = 1.5f; 

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 
    }

    void Update()
    {
       
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -45f, 45f); 

        rotationY += mouseX;

        
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        
        Vector3 targetPosition = player.position - transform.forward * distanceFromPlayer + Vector3.up * heightOffset;
        transform.position = targetPosition;
    }
}