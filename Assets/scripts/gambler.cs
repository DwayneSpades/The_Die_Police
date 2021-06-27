using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gambler : MonoBehaviour
{
    public dice die;
    public Animator gambler_anim;
    public int money_score = 0;
    public float reactionTime = 0;
    public int anger = 0;

    public bool swipingDice = false;
    public float swipeTimer;
    public float swipeTime;

    public bool throwingDice = false;
    public float throwTimer;
    public float throwTime;

    // Start is called before the first frame update
    void Start()
    {
        
        //gambler_anim.Play("larry_throw");
    }

    // Update is called once per frame
    void Update()
    {
        if(throwingDice)
            throwTimer -= 1 * Time.deltaTime;

        if (throwingDice && throwTimer <= 0)
        {
            throwTimer = 0;
            throwingDice = false;
            Instantiate(die, new Vector2(2, 0), Quaternion.identity);
        }

        if (swipingDice)
            swipeTimer -= 1 * Time.deltaTime;

        if (swipingDice && swipeTimer <= 0)
        {
            swipeTimer = 0;
            swipingDice = false;
            Instantiate(die, new Vector2(2, 0), Quaternion.identity);
        }

    }


    public void rollDice()
    {
        gambler_anim.Play("larry_throw");
        throwTimer = throwTime;
        throwingDice = true;
    }

    public void swipeDice()
    {
        gambler_anim.Play("larry_swipe");
        swipeTimer = swipeTime;
        swipingDice = true;
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
