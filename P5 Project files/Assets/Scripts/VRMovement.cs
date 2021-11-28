using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class VRMovement : MonoBehaviour
{
    //VR input specified in inspector
    [SerializeField] private XRNode inputSource;
    
    private Vector2 inputAxis;
    private CharacterController character;
    private XROrigin rig;

    [SerializeField] private float acceleration = 0.7f;
    [SerializeField] private float maxSpeed = 4f;
    private float speed = 0f;
    private bool triggerValue;

    private void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }
    
    private void Update()
    {
        //Getting input from VR devices
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        
        // Is there a trigger input on controller?
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out triggerValue) && triggerValue)
        {
        }

        // Acceleration
        if (speed < maxSpeed && triggerValue)
        {
            speed += acceleration * Time.fixedDeltaTime;
        }
        // Deceleration
        if (speed > 0 && !triggerValue)
        {
            speed -= acceleration * Time.fixedDeltaTime;
        }
    }

    private void FixedUpdate()
    {
        //turns character controller depending on where the camera is looking
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);

        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * Time.fixedDeltaTime * speed);
    }
}