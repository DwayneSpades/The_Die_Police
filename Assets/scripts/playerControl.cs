using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    //public GameObject playerObject;
    public gambler playerStat;
    public police theFeds;

    public float dicePower = 0;

    bool throwingDice = false;
    bool diceDown = true;
    bool swiping = true;

    public bool hideGame = false;

    


    // Start is called before the first frame update
    void Start()
    {

       
        throwingDice = false;
        hideGame = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            throwingDice = false;
            dicePower = 0;
            hideGame = true;
            //theFeds.nothingSus();

            Debug.Log("Hiding Game!!!!");
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            hideGame = false;
            Debug.Log("Game ON!!!!");
        }

        // throw the dice and get a number
        if (!hideGame)
        {
            //trigger dice throw
            if (Input.GetKeyDown(KeyCode.X) && !throwingDice)
            {
                throwingDice = true;
                Debug.Log("ROLLING DICE");
            }

            //while dice throw is being charged up
            if (throwingDice)
            {
                dicePower += 1 * Time.deltaTime;
            }

            //throw dice
            if (Input.GetKeyUp(KeyCode.X) && throwingDice)
            {
                throwingDice = false;
                
                Debug.Log("DICE THROWN! Power: " + dicePower);
                Debug.Log("DICE NUMBER: " + Random.Range(2,13));
                addSus(dicePower);
                dicePower = 0;
            }

            //swipe dice after they are thrown
            if (Input.GetKeyUp(KeyCode.C) && throwingDice)
            {
                swiping = true;
            }
        }
        

    }

    void addSus(float amount)
    {
        theFeds.sus += amount;
        theFeds.checkBar();
    }

    void resetMatch()
    {
        dicePower = 0;
        throwingDice = false;
        diceDown = true;
        swiping = true;
    }

    void resetGame()
    {
        gameTable tmp = FindObjectOfType<gameTable>();
        tmp.triggerReset();

    }
}
