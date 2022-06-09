using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    private NavMeshSurface surface;

    // Start is called before the first frame update
    public void Start()
    {
        GameObject spawner = GameObject.Find("Spawner");
        surface = spawner.GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
