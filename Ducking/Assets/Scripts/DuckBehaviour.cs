using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class DuckBehaviour : MonoBehaviour
{
    GameObject spawner;

    public NavMeshAgent agent;

    Material duckMaterial;
    private float materialRedness;
    float rednessIncrement;
    [Range(1f, 10f)]
    public float rednessIncrementFactor;
    string rednessReference;

    [Range(0,500)]
    public float radius;

    
    public float lifetimeInterval;
    private float lifetime;

    ParticleSystem confettiParticle;
    public ParticleSystem explosionParticle;

    ParticleSystem confettiParticleInstance;
    ParticleSystem explosionParticleInstance;

    void Start()
    {
        spawner = GameObject.Find("Spawner");
        if (spawner == null)
            Debug.LogWarning("Could find spawner");

        confettiParticle = GameObject.Find("confetti").GetComponent<ParticleSystem>();
        if (confettiParticle == null)
            Debug.LogWarning("Could find confetti");

        //explosionParticle = GameObject.Find("explosion").GetComponent<ParticleSystem>();
        //if (explosionParticle == null)
        //    Debug.LogWarning("Could find explosion");

        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = Random.Range(1f,3f);

            if(Random.Range(0,1) == 0f)
               lifetime = Random.Range(lifetimeInterval / 3f, lifetimeInterval);
            else
                lifetime = Random.Range(lifetimeInterval / 1.5f, lifetimeInterval);

            agent.SetDestination(RandomNavMeshLocation());
        }

        //Duck Material Instance
        var renderer = GetComponentInChildren<Renderer>();
        duckMaterial = Instantiate(renderer.sharedMaterial);
        renderer.material = duckMaterial;

        //Explosion Particle Instance
        materialRedness = 0f;
        rednessIncrement = 5f/lifetime;
        rednessReference = "Vector1_032a385f8a344deb803012daf7caf1af";
        duckMaterial.SetFloat(rednessReference, materialRedness);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent!=null && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(RandomNavMeshLocation());
        }

        if(materialRedness <= 5f)
        {
            materialRedness += rednessIncrement * Time.deltaTime;
            duckMaterial.SetFloat(rednessReference, materialRedness);
        }


        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Debug.Log("Duck Die");

            Instantiate(explosionParticle, transform.position, transform.rotation);
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
        else
        {
            RandomNavMeshLocation();
        }


        return finalPos;
    }
}

