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

    GameObject progressBar;
    float progressIncrement;
    float progressDecrement;
    void Start()
    {

        progressIncrement = 0.001f;
        progressDecrement = progressIncrement * 3;

        cursor = GameObject.Find("cursor");
        if (cursor == null)
            Debug.LogWarning("Couldnt find cursor");

        progressBar = GameObject.Find("Progress Bar");
        if (progressBar == null)
            Debug.LogWarning("Couldnt find progressBar");

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

        UpdatePrpgressBar();
    }

    void UpdatePrpgressBar()
    {
        if (progressBar.GetComponent<Slider>().value <= 0.1f)
            progressBar.GetComponent<Slider>().value = 0.1f;



        if (duckDetected)
        {
            Debug.LogWarning("Progress Bar");

            progressBar.GetComponent<Slider>().value += progressIncrement;
        }
        else
            progressBar.GetComponent<Slider>().value -= progressDecrement;


    }
     
}
