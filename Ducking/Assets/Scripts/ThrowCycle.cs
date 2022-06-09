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

    // Start is called before the first frame update
    void Start()
    {
        rodScript = GameObject.Find("fish_rod").GetComponent<RodBehaviour>();
        net = GameObject.Find("Net");
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * initialForce / 1.5f);
        rb.AddForce(transform.forward * initialForce * rodScript.currentForceMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rodScript.currentForceMultiplier = 0f;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Vector3 dir = Camera.main.transform.position - transform.position;
            dir = dir.normalized;
            if(rodScript.pressing)
            {
                rodScript.RotateHandle();
                rb.AddForce(dir * rodScript.currentForceMultiplier * 20f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Duck")
        {
            if (other.GetComponent<DuckBehaviour>().invincible) return;
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
