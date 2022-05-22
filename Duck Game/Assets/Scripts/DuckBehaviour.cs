using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]


public class DuckBehaviour : MonoBehaviour
{

    public NavMeshAgent agent;

    
    [Range(0,500)]
    public float radius;

    void Awake()
    {

        agent = GetComponent<NavMeshAgent>(); 
        if(agent!=null)
        {
            agent.speed= Random.Range(1f,20f);
            Debug.Log("Speed set to: " + agent.speed);
            agent.SetDestination(RandomNavMeshLocation());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(agent!=null && agent.remainingDistance<=agent.stoppingDistance)
        {
            agent.SetDestination(RandomNavMeshLocation());
        }
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPos = Vector3.zero;
        Vector3 randomPos = Random.insideUnitSphere * radius;

        randomPos+=transform.position;

        if(NavMesh.SamplePosition(randomPos,out NavMeshHit hit, radius,1))
        {
            finalPos=hit.position;
        }

        return finalPos;
    }
}

