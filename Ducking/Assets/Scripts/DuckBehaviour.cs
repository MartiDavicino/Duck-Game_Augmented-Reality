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
    float rednessIncrement;
    string rednessReference;

    [Range(0,500)]
    public float radius;

    float lifetime;

    ParticleSystem confettiParticle;
    ParticleSystem explosionParticle;
    void Awake()
    {
        spawner = GameObject.Find("Spawner");
        if (spawner == null)
            Debug.LogWarning("Could find spawner");

        confettiParticle = GameObject.Find("confetti").GetComponent<ParticleSystem>();
        if (confettiParticle == null)
            Debug.LogWarning("Could find confetti");

        explosionParticle = GameObject.Find("explosion").GetComponent<ParticleSystem>();
        if (explosionParticle == null)
            Debug.LogWarning("Could find explosion");

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            //agent.speed= Random.Range(1f,5f);
            agent.speed = 1f;
            lifetime = Random.Range(7f, 25f);
            //Debug.Log("Speed set to: " + agent.speed);
            agent.SetDestination(RandomNavMeshLocation());
        }


        var renderer=GetComponentInChildren<Renderer>();
        duckMaterial=Instantiate(renderer.sharedMaterial);
        renderer.material=duckMaterial;


        materialRedness = 0f;
        rednessIncrement = (5.5f/lifetime)/1000;
        rednessReference = "Vector1_032a385f8a344deb803012daf7caf1af";
        duckMaterial.SetFloat(rednessReference, materialRedness);

        //confettiParticle.Pause();
        //explosionParticle.Pause();

    }

    // Update is called once per frame
    void Update()
    {
        

        if (agent!=null && agent.remainingDistance<=agent.stoppingDistance)
        {
            agent.SetDestination(RandomNavMeshLocation());
        }

        if(materialRedness<=2f)
        {
            materialRedness += rednessIncrement;
            duckMaterial.SetFloat(rednessReference, materialRedness);
        }
        

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Debug.Log("Duck Die");

            spawner.GetComponent<DuckSpawner>().currentDucks--;

            if(explosionParticle!=null)
                explosionParticle.Play();

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

