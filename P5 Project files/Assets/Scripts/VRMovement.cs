using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRMovement : MonoBehaviour
{
    //VR input specified in inspector
    public XRNode inputSource;
    private Vector2 inputAxis;

    private CharacterController character;

    // private float speed = 1;
    private XRRig rig;
    Vector3 playerMovement;
    public float moveSpeed = 10;

    private float acceleration = 0.7f;
    private float maxSpeed = 4f;
    private float speed = 0f;
    bool triggerValue;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        //Getting input from VR devices
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisTouch, out triggerValue) && triggerValue)
        {

        }

        if (speed < maxSpeed)
        {
            speed += acceleration * Time.fixedDeltaTime;
        }
    }

    private void FixedUpdate()
    {
        //turns charactercontroller depending on where the camera is looking
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);

        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * Time.fixedDeltaTime * speed);
    }
}