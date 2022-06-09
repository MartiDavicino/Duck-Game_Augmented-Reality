using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public int currentScore;

    public List<GameObject> hats;
    public List<GameObject> unlockedHats;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyUp(KeyCode.F1))
        {
            currentScore += 100;
        }

        for(int i = 0; i < hats.Count; i++)
        {
            int price = 100 + 100 * i;

            if(currentScore >= price)
            {
                if (unlockedHats.Count == 0)
                    unlockedHats.Add(hats[i]);

                if (!unlockedHats.Contains(hats[i]))
                {
                    unlockedHats.Add(hats[i]);
                }
            }
        }
    }
}
