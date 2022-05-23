using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    [Range(1, 50)]
    public uint numberOfDucks;
    public uint currentDucks;
    public Transform initialPos;
    [Range(0, 500)]
    public float radius;

    float respawnTime=0.5f;
    float respawnCounter = 0f;

    public GameObject duckPrefab;
    // Start is called before the first frame update
    void Start()
    {
        currentDucks = numberOfDucks;

        for(int i = 0; i < numberOfDucks; i++)
        {
            Instantiate(duckPrefab,RandomPosition(),Quaternion.identity);
        }
    }

    private void Update()
    {
        //DucksCount();

        if(currentDucks < numberOfDucks)
        {
            respawnCounter+=Time.deltaTime;

            if(respawnCounter>respawnTime)
            {
                SpawnNewDuck();
            }
        }
    }
    Vector3 RandomPosition()
    {
        Vector3 randomPostion = Vector3.zero;
        randomPostion.x = Random.Range(-radius,radius);
        randomPostion.z = Random.Range(-radius, radius);
        randomPostion.y=initialPos.position.y;  

        return randomPostion;

    }

    uint DucksCount()
    {
        Debug.Log("Nummber of ducks: "+currentDucks);
        return numberOfDucks;
    }

    void SpawnNewDuck()
    {
        Instantiate(duckPrefab, RandomPosition(), Quaternion.identity);
        currentDucks++;
        respawnCounter = 0f;
    }


}
