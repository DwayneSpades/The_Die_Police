using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class police : MonoBehaviour
{
    public playerControl player;
    public float sus = 0;
    public float maxSus = 0;
    public float susThreshold = 50;

    public bool checking = false;
    public bool patrolling =false;


    public float checkTimer;
    public float checkTime;

    // Start is called before the first frame update
    void Start()
    {
        checkTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (checkTimer <= 0 && checking && !player.hideGame)
        {
            checkTimer = 0;
            checking = false;
            Debug.Log("YOU LOSE!!!!");
        }
        else if (checkTimer <= 0 && checking && player.hideGame)
        {
            checkTimer = 0;
            checking = false;
            Debug.Log("SAFE");
        }
        else if(checking)
        {
            checkTimer -= 1 * Time.deltaTime;
        }
    }

    public void nothingSus()
    {

        Debug.Log("hhmmst");
    }

    public void checkBar()
    {
        float verdict = Random.Range(sus,maxSus);
        if (verdict >susThreshold)
        {
            checking = true;
            checkTimer = checkTime;
            Debug.Log("SECURITY CHECK!!!");
        }


    }
}
