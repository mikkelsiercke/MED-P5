using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 5.0f;

    private Transform myTransform; 
        
    private void Start()
    {
        myTransform = gameObject.transform;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;
        
        //Forward and sideways movement
        Vector3 move = myTransform.right * x + myTransform.forward * z;

        //Attach movement + controllable speed to specified controller in inspector
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
