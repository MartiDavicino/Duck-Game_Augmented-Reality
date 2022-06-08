using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DuckSpawner : MonoBehaviour
{
    [Range(1, 50)]
    public uint numberOfDucks;
    public uint currentDucks;
    public Transform initialPos;
    [Range(0, 500)]
    public float radius;

    float respawnTime = 1.5f;
    float respawnCounter = 0f;

    public GameObject duckPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //currentDucks = numberOfDucks;

        for(int i = 0; i < numberOfDucks; i++)
        {
           // Instantiate(duckPrefab,RandomPosition(),Quaternion.identity);
        }
    }

    private void Update()
    {
        //DucksCount();

        if(currentDucks < numberOfDucks)
        {
            respawnCounter += Time.deltaTime;

            if(respawnCounter>respawnTime)
            {
                SpawnNewDuck();
            }
        }
    }
    public Vector3 RandomPosition()
    {
        Vector3 randomPostion = Vector3.zero;
        randomPostion.x = Random.Range(-radius, radius);
        randomPostion.z = Random.Range(-radius, radius);
        randomPostion.y = initialPos.position.y + 0.5f;  

        return randomPostion;
    }
   
    uint DucksCount()
    {
        Debug.Log("Nummber of ducks: "+currentDucks);
        return numberOfDucks;
    }

    void SpawnNewDuck()
    {
        Instantiate(duckPrefab, RandomNavMeshLocation(), Quaternion.identity);

        currentDucks++;
        respawnCounter = 0f;
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPos = Vector3.zero;
        Vector3 randomPos = Random.insideUnitSphere * radius;

        randomPos += transform.position;

        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, radius, 1))
        {
            finalPos = hit.position;
        } else
        {
            RandomNavMeshLocation();
        }

        return finalPos;
    }
}
