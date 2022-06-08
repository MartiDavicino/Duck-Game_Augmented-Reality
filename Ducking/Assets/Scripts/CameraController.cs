using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraController : MonoBehaviour
{
    [Range(1f, 10f)]
    public float mouseSensitivity;

    GameObject UI;
    GameObject rodHandle;
    float rodRotationSpeed;
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
        rodRotationDecrease = rodRotationIncrease * 3.5f;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        RotateHandle();

        GetInputLookRotation();

        if (RayCastDuckDetection())
        {
            UI.GetComponent<UIController>().duckDetected = true;
            if (rodRotationSpeed < 4)
                rodRotationSpeed += rodRotationIncrease * Time.deltaTime;
        }
            
        else
        {
            UI.GetComponent<UIController>().duckDetected = false;
            if (rodRotationSpeed > 0)
                rodRotationSpeed -= rodRotationDecrease * Time.deltaTime;
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
                Fish(selection.gameObject);
                ret = true;
            }
        }
        return ret;
    }

    public void Fish(GameObject duck)
    {
        duck.GetComponent<DuckBehaviour>().caughtTimeIncrease += Time.deltaTime;
        duck.GetComponent<DuckBehaviour>().caught = true;

        if (duck.GetComponent<DuckBehaviour>().caughtTimeIncrease >= duck.GetComponent<DuckBehaviour>().caughtTime)
        {
            float step = rodRotationSpeed * Time.deltaTime;
            duck.transform.position = Vector3.MoveTowards(duck.transform.position, transform.position, step);
            duck.GetComponent<NavMeshAgent>().ResetPath();
            duck.GetComponent<DuckBehaviour>().caughtTimeIncrease = duck.GetComponent<DuckBehaviour>().caughtTime;
        }
    }
    void RotateHandle()
    {
        rodHandle.transform.Rotate(0, 0, -rodRotationSpeed);
    }
}
