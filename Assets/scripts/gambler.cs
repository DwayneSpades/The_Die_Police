using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gambler : MonoBehaviour
{

    public int money_score = 0;
    public float reactionTime = 0;
    public int anger = 0;
    public bool swiped = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int rollDice()
    {
        return Random.Range(1, 7);
    }

    public void addMoney(int amount)
    {
        money_score = money_score + amount;
    }

    public void loseMoney(int amount)
    {
        money_score = money_score - amount;
    }

}
