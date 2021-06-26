using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTable : MonoBehaviour
{

    int gameReset = 10;
    int currentTime = 0;
    bool resetMatch =false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (resetMatch)
        {
            if (currentTime >= gameReset)
            {

            }
        }
    }

    public void triggerReset()
    {
        resetMatch = true;

    }
}
