using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodBehaviour : MonoBehaviour
{
    private Camera arCamera;
    public float offset;

    public GameObject baitPrefab;
    private GameObject bait;

    public float maxForceMultiplier;
    [HideInInspector] public float currentForceMultiplier;

    private GameObject rod;
    private RodBehaviour rodScript;

    [HideInInspector] public bool pressing;

    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        arCamera = Camera.main;

        currentForceMultiplier = 0f;
        pressing = false;

        rod = GameObject.Find("handle");
        rodScript = rod.GetComponent<RodBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = arCamera.transform.position + arCamera.transform.forward * offset;
        if(transform.position.x <= 0)
        {
            transform.position = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z);
        } else
        {
            transform.position = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
        }

        if (Input.touchCount > 0)
        {
            Touch first = Input.GetTouch(0);

            if (first.phase == TouchPhase.Stationary)
            {
                pressing = true;

                if (currentForceMultiplier < maxForceMultiplier)
                {
                    currentForceMultiplier = currentForceMultiplier + Time.deltaTime;
                }
                else
                {
                    currentForceMultiplier = maxForceMultiplier;
                }
            }

            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                pressing = false;


                spawnPos = arCamera.transform.position + arCamera.transform.forward * offset;

                if (bait == null)
                {
                    bait = Instantiate(baitPrefab, spawnPos, Camera.main.transform.rotation);
                }
            }

            if(Input.touchCount > 1)
            {
                if (bait != null)
                {
                    currentForceMultiplier = 0f;
                    Destroy(bait);
                }
            }
        }

        if (!pressing)
        { 
            if(currentForceMultiplier > 0f)
                currentForceMultiplier -= Time.deltaTime;
            else if (currentForceMultiplier < 0f)
                currentForceMultiplier = 0f;
        }
         
        
             
    }

    public void RotateHandle()
    {
        rod.transform.Rotate(0, 0, currentForceMultiplier * 5f);
    }
}
