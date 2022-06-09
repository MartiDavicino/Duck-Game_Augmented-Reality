using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArController : MonoBehaviour
{
    public GameObject myObject;
    public ARRaycastManager raycastManager;

    GameObject myCamera;
    GameObject ARSession;

    bool planePlaced=false;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            List<ARRaycastHit> touches = new List<ARRaycastHit>();

            raycastManager.Raycast(Input.GetTouch(0).position, touches, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (!planePlaced)
            {
                GameObject.Instantiate(myObject, touches[0].pose.position, touches[0].pose.rotation);
                planePlaced = true;
            }
        }

        if (planePlaced && myCamera==null)
        {

            myCamera = GameObject.Find("Main Camera");
            ARSession = GameObject.Find("AR Session Origin");

            ARPlaneManager planeManager = ARSession.GetComponent<ARPlaneManager>();
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }

            ARSession.GetComponent<ARSessionOrigin>().camera=myCamera.GetComponent<Camera>();
        }
    }
}
