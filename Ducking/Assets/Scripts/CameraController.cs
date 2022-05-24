using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(1f, 10f)]
    public float mouseSensitivity;

    GameObject UI;
    GameObject rodHandle;
    float rodRotationSpeed;
    [Range(0.001f, .1f)]
    public float rodRotationIncrease;
    float rodRotationDecrease;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("UI");
        if (UI == null)
            Debug.LogWarning("Couldnt find UI");

        rodHandle = GameObject.Find("handle");
        if (rodHandle == null)
            Debug.LogWarning("Couldnt find rod");

        rodRotationSpeed = 0f;
        rodRotationDecrease = rodRotationIncrease * 3f;
    }

    // Update is called once per frame
    void Update()
    {
        RotateHandle();

        GetInputLookRotation();

        if (RayCastDuckDetection())
        {
            UI.GetComponent<UIController>().duckDetected = true;
            if (rodRotationSpeed < 1)
                rodRotationSpeed += rodRotationIncrease;
        }
            
        else
        {
            UI.GetComponent<UIController>().duckDetected = false;
            if (rodRotationSpeed > 0)
                rodRotationSpeed -= rodRotationDecrease;
        }
        

        //Debug.Log("Current speed: "+rodRotationSpeed);
    }

    void  GetInputLookRotation()
    {
        float yaw = Input.GetAxis("Mouse X") * mouseSensitivity;
        float pitch = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(-pitch, yaw, 0);
        

    }

    bool RayCastDuckDetection()
    {
        bool ret = false;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            var selection = hit.transform;
            if(selection.tag=="Duck")
            {
                ret = true;
                Debug.Log("Duck hit");
            }
            
        }
        return ret;
    }

    void RotateHandle()
    {
        rodHandle.transform.Rotate(0, 0, -rodRotationSpeed);
    }
}
