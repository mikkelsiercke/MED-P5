using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSens = 1000.0f;
    public Transform playerBody;

    private float xRotation;

    // Start is called before the first frame update
    private void Start()
    {
        // Allows hiding the mouse cursor during play
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        // Mouse Rotation - * Time.deltaTime to make sure that rotation speed is unaffected by
        // framerate (high framerate = faster rotation)
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        // is - mouseY else rotation is inverse for some reason
        xRotation -= mouseY;
        // Constraints rotation se the player cannot rotate camera 360 up and down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // Quaternions are responsible for rotation in unity
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Left and right Rotation - around Y-axis
        playerBody.Rotate(Vector3.up * mouseX);
    }
}