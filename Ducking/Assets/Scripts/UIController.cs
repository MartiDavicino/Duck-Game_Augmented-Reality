using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite cursor01;
    public Sprite cursor02;

    [HideInInspector]
    public bool duckDetected;

    GameObject cursor;
    void Start()
    {
        cursor = GameObject.Find("cursor");
        if (cursor == null)
            Debug.LogWarning("Couldnt find cursor");

        cursor.GetComponent<Image>().sprite= cursor01;
        duckDetected = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (duckDetected && cursor.GetComponent<Image>().sprite != cursor02)
            cursor.GetComponent<Image>().sprite = cursor02;

        else if (!duckDetected && cursor.GetComponent<Image>().sprite != cursor01)
            cursor.GetComponent<Image>().sprite = cursor01;
    }

     
}
