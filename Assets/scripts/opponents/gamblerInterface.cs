using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamblerInterface : MonoBehaviour
{
    //I wanted this to be protected and serializable but that doenst work???
    public string loseScreen;

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


    void Awake()
    {
        anger = 0;
    }

    //animation state controlls
    
    public void addAnger()
    {
        anger += angerAcceleration;
        if (anger >= angerLimit)
        {
            SceneManager.LoadScene(loseScreen);
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
