using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodBehaviour : MonoBehaviour
{
    private Camera arCamera;
    public float offset;

    public GameObject baitPrefab;
    private GameObject bait;
    // Start is called before the first frame update
    void Start()
    {
        arCamera = Camera.main;

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
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                
            }
            {
                if(bait == null)
                    bait = Instantiate(baitPrefab, transform.position, Quaternion.identity);
            }
        }

        if (Input.GetMouseButton(0))
        {
            transform.Rotate(Vector3.up * Time.deltaTime);
        }
    }
}
