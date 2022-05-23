using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]


public class DuckBehaviour : MonoBehaviour
{
    GameObject spawner;

    public NavMeshAgent agent;

    Material duckMaterial;
    float materialRedness;
    string rednessReference;

    [Range(0,500)]
    public float radius;

    float lifetime;

    void Awake()
    {
        spawner = GameObject.Find("Spawner");
        if (spawner == null)
            Debug.LogWarning("Could find spawner");


        var renderer=GetComponentInChildren<Renderer>();
        duckMaterial=Instantiate(renderer.sharedMaterial);
        renderer.material=duckMaterial;


        materialRedness = 0f;
        rednessReference = "Vector1_032a385f8a344deb803012daf7caf1af";
        duckMaterial.SetFloat(rednessReference, materialRedness);


        agent = GetComponent<NavMeshAgent>(); 
        if(agent!=null)
        {
            //agent.speed= Random.Range(1f,5f);
            agent.speed = 1f;
            lifetime = Random.Range(3f, 10f);
            //Debug.Log("Speed set to: " + agent.speed);
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

        if(materialRedness<=2f)
        {
            materialRedness += .001f;
            duckMaterial.SetFloat(rednessReference, materialRedness);
        }
        

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Debug.Log("Duck Die");

            spawner.GetComponent<DuckSpawner>().currentDucks--;

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(duckMaterial!=null)
            Destroy(duckMaterial);
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

