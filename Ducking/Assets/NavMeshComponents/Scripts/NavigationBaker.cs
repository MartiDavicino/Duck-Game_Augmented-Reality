using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    public NavMeshSurface surface;

    // Start is called before the first frame update
    public void Start()
    {
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
