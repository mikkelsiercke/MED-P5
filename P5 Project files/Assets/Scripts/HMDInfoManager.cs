using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        switch (XRSettings.isDeviceActive)
        {
            case false:
                Debug.Log("No Headset plugged");
                break;
            case true when (XRSettings.loadedDeviceName == "Mock HMD"
                            || XRSettings.loadedDeviceName == "MockHDMDisplay"):
                Debug.Log("Using Mock HDM");
                break;
            default:
                Debug.Log("We have a headset " + XRSettings.loadedDeviceName);
                break;
        }
    }
}