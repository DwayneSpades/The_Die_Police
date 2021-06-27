using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{




    public static gameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int currentLevel = 1;

    public int diceTotal;

    void Start()
    {
        Instance.diceRoll = new List<dice>();
    }

    public List<dice> diceRoll;

    public void collectDice(dice die)
    {
        Instance.diceTotal += die.dice_number;
        //Debug.Log("ADDING NUM:" + diceTotal);
        diceRoll.Add(die);
    }
    public void resetDiceRoll()
    {
        for(int i=0; i < diceRoll.Count; i++)
        {
            diceRoll[i].destroyDice(); ;
        }
        diceTotal = 0;
        diceRoll.Clear();
    }
    public int giveRollTotal()
    {
        Debug.Log("total dice points: " + Instance.diceTotal);
        return Instance.diceTotal;
    }
   
}
