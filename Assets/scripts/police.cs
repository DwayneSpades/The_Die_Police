using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class police : MonoBehaviour
{

    public Sprite checkingDoor;
    public Sprite Alerted;

    public playerCon player;
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
        gameObject.active = false;
        checkTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (checkTimer <= 0 && checking && !player)
        {
            checkTimer = 0;
            checking = false;
            GetComponent<SpriteRenderer>().sprite = Alerted;
            Debug.Log("YOU LOSE!!!!");
            SceneManager.LoadScene("loseScreen");
        }
        else if (checkTimer <= 0 && checking && player.hidingGame)
        {
            //sus check passed. reset sus stat
            checkTimer = 0;
            checking = false;
            gameObject.active = false;
            sus = 0;
            player.resetAnger();
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
            gameObject.active = true;
            checking = true;
            checkTimer = checkTime;
            GetComponent<SpriteRenderer>().sprite = checkingDoor;
            Debug.Log("SECURITY CHECK!!!");
        }


    }
}
