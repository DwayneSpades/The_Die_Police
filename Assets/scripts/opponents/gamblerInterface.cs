using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamblerInterface : MonoBehaviour
{

    //gambler stats
    [HideInInspector]
    public int anger = 0;

    [SerializeField]
    protected dice die;

    public int angerAcceleration;
    public int angerLimit;

    public float reactionTimeHigh;
    public float reactionTimeLow;

    protected float chargeTimer;
    public float chargeTime;

    protected bool chargingRoll = false;

    [SerializeField]
    protected Animator gambler_anim;

    public int money_score;
    public int BettingAmount;

    void Awake()
    {
        anger = 0;
    }

    public void addMoney(int amount)
    {
        money_score += amount;
    }

    public void loseMoney(int amount)
    {
        money_score -= amount;
    }

    public void addAnger()
    {
        anger += angerAcceleration;
        if (anger >= angerLimit)
        {
            SceneManager.LoadScene(gameManager.Instance.loseScreen);
        }
    }

    public void resetAnger()
    {
        anger = 0;
    }

    public virtual void readyToROll()
    {

    }

    public virtual bool chargeRoll()
    {
        return false;
    }
    public virtual void rollDice()
    {

    }
    public virtual bool runSwipeTimer()
    {
        return false;
    }
    public virtual void swipeDice()
    {

    }

    public virtual void missedSwipe()
    {

    }
    public virtual void angered()
    {

    }

    public virtual void talk()
    {

    }

}
