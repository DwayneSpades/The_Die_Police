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
    public List<dice> diceRoll = new List<dice>();


    void Start()
    {
 
    }

    
    public void collectDice(dice die)
    {
        diceTotal += die.dice_number;
        Debug.Log("total dice points: " + diceTotal);
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
