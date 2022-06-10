using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThrowCycle : MonoBehaviour
{
    public float initialForce;

    private float rodRotationSpeed = 15f;

    private Rigidbody rb;
    private GameObject net;

    private RodBehaviour rodScript;
    private bool pressing;
    // Start is called before the first frame update
    void Start()
    {
        net = GameObject.Find("Net");
        rodScript = GameObject.Find("fish_rod").GetComponent<RodBehaviour>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * initialForce / 1.5f);
        rb.AddForce(transform.forward * initialForce * rodScript.currentForceMultiplier);
    }

    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rodScript.currentForceMultiplier = 0f;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity /= 5f;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {
            if (Input.touchCount > 0)
            {
                Touch first = Input.GetTouch(0);

                if (first.phase == TouchPhase.Stationary)
                {
                    Vector3 dir = Camera.main.transform.position - transform.position;
                    dir.y = transform.position.y;
                    dir = dir.normalized;
                    rb.AddForce(dir * 45f);
                    //var step = 10f * Time.deltaTime; // calculate distance to move
                    //transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, step);
                }
            } else
            {
                if (rb.velocity.magnitude > 1f)
                {
                    rb.velocity -= rb.velocity/2;
                }
            }


            //if (rodScript != null && rodScript.pressing)
            //{
                //rodScript.RotateHandle();
            //}


            //if(rodScript != null)
            //{
            //    rodScript.RotateHandle();
            //    var step = 5f * Time.deltaTime; // calculate distance to move
            //    transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, step);
            //}
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Duck")
        {
            if (other.GetComponent<DuckBehaviour>().invincible) return;
            other.GetComponent<DuckBehaviour>().caught = true;
            other.transform.position = transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Duck")
        {
            other.GetComponent<NavMeshAgent>().ResetPath();
            other.transform.position = transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Duck")
        {
            if (other.GetComponent<DuckBehaviour>().invincible) return;
            other.GetComponent<DuckBehaviour>().caught = true;
            //Destroy(other.GetComponent<NavMeshAgent>());
            other.transform.position = transform.position;
        }
        
        if (other.tag == "DeathBait")
        {
            Destroy(gameObject);
        }
    }
}
