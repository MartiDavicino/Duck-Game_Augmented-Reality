using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(1f, 10f)]
    public float mouseSensitivity;

    GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("UI");
        if (UI == null)
            Debug.LogWarning("Couldnt find UI");
    }

    // Update is called once per frame
    void Update()
    {

        GetInputLookRotation();

        if (RayCastDuckDetection())
            UI.GetComponent<UIController>().duckDetected = true;
        else
            UI.GetComponent<UIController>().duckDetected = false;

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
}
