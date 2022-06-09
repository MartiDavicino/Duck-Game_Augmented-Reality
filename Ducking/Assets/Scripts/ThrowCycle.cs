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

    // Start is called before the first frame update
    void Start()
    {
        net = GameObject.Find("Net");
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * initialForce / 1.5f);
        rb.AddForce(transform.forward * initialForce);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Vector3 dir = net.transform.position - transform.position;
            dir = dir.normalized;
            rb.AddForce(dir * rodRotationSpeed);
        }

        

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Duck")
        {
            other.GetComponent<DuckBehaviour>().caught = true;
            Destroy(other.GetComponent<NavMeshAgent>());
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
        if(other.tag == "DeathBait")
        {
            Destroy(gameObject);
        }
    }
}
