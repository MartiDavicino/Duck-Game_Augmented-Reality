using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    private Text counterText;
    
    private GeneralManager generalManager;

    // Start is called before the first frame update
    void Start()
    {
        counterText = gameObject.GetComponent<Text>();

        generalManager = GameObject.Find("fish_rod").GetComponent<GeneralManager>();

    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = generalManager.currentScore.ToString();
    }
}
