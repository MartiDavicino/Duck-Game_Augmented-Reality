using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    [Range(1, 50)]
    public uint numberOfDucks;

    public Transform initialPos;
    [Range(0, 500)]
    public float radius;

    public GameObject duckPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberOfDucks; i++)
        {
            Instantiate(duckPrefab,RandomPosition(),Quaternion.identity);
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
}
