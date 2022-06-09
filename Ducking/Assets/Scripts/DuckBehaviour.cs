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

    public ParticleSystem confettiParticle;
    public ParticleSystem explosionParticle;

    ParticleSystem confettiParticleInstance;
    ParticleSystem explosionParticleInstance;

    [HideInInspector] public bool caught;

    [HideInInspector] public bool invincible;

    [HideInInspector] public float caughtTime = 0.5f;
    [HideInInspector] public float caughtTimeIncrease;

    private List<GameObject> hats;
    private GeneralManager generalManager;
    private int hatIndex;
    private GameObject hat;

    private bool aTomarPorCulo;

    void Start()
    {
        spawner = GameObject.Find("Spawner");
        if (spawner == null)
            Debug.LogWarning("Could find spawner");

        caughtTimeIncrease = 0f;

        agent = GetComponent<NavMeshAgent>();

        //float duckRandom = Random.Range(0f, 1f);

        if (Random.Range(0f, 1f) >= 0.65f)
        {
            if (Random.value > 0.5f)
            {
                Vector3 randomSize = new Vector3(1, 1, 1) * Random.Range(0.6f, 0.7f);
                transform.localScale = randomSize;
                agent.speed = 8f;
            }
            else
            {
                Vector3 randomSize = new Vector3(1, 1, 1) * Random.Range(1.7f, 2f);
                transform.localScale = randomSize;
                agent.speed = 0.5f;
            }
        }

        if (agent != null)
        {

            if(Random.value > 0.5f)
               lifetime = Random.Range(lifetimeInterval / 5f, lifetimeInterval / 2f);
            else
                lifetime = Random.Range(lifetimeInterval / 2f, lifetimeInterval);

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

        aTomarPorCulo = false;


        invincible = false;

        generalManager = GameObject.Find("fish_rod").GetComponent<GeneralManager>();
        if(generalManager.unlockedHats.Count > 0)
        {
            hats = generalManager.unlockedHats;

            hatIndex = Random.Range(0, hats.Count);
            Vector3 startPosition = new Vector3(transform.position.x, transform.position.y + (0.9f * transform.localScale.y), transform.position.z + (0.15f * transform.localScale.z));
            hat = Instantiate(hats[hatIndex], startPosition, Quaternion.identity);
            hat.transform.parent = gameObject.transform;
            hat.transform.Rotate(-90, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (agent!=null && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(RandomNavMeshLocation());
        }

        if(materialRedness <= 5f && !invincible)
        {
            materialRedness += rednessIncrement * Time.deltaTime;
            duckMaterial.SetFloat(rednessReference, materialRedness);
        }

        if(!caught && caughtTimeIncrease > 0)
            caughtTimeIncrease -= Time.deltaTime;


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
    private Vector3 RandomNavMeshLocation()
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Net" && caught)
        {
            generalManager.currentScore += 100;
            //generalManager.currentScore += 30 / (int)transform.localScale.magnitude;
            Instantiate(confettiParticle, transform.position, transform.rotation);
            spawner.GetComponent<DuckSpawner>().currentDucks--;
            aTomarPorCulo = true;
        } else if (other.tag == "Net")
        {
            invincible = true;
            duckMaterial.SetFloat(rednessReference, -10);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Net")
            invincible = false;
    }

    private void LateUpdate()
    {
        if(aTomarPorCulo)
            Destroy(gameObject);
    }
}

