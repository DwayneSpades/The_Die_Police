using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazy_larry : gamblerInterface
{
    

    //gambler stats
    //[SerializeField]
    //int anger = 0;

    // public int angerAcceleration;
    // public float reationTimeHigh;
    //public float reactionTimeLow;

    void Awake()
    {
        anger = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        soundManager.Instance.playMainTheme();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void playThemeSong()
    {
        
    }

    public override bool chargeRoll()
    {
        if (!chargingRoll)
        {
            gambler_anim.Play("larry_charge");
            chargeTimer = chargeTime;
            chargingRoll = true;
        }  
        else
        {
            chargeTimer -= 1 * Time.deltaTime;
            if (chargeTimer <= 0)
            {
                chargeTimer = 0;
                chargingRoll = false;
                rollDice();
                return true;
            }
        }
        return false;
    }

    //animation state controlls
    public override void rollDice()
    {
        gambler_anim.Play("larry_roll");

        Vector2 tmp_v = new Vector2(2, 0);
        Instantiate(die, tmp_v, Quaternion.identity);
    }

    public override bool runSwipeTimer()
    {
        if (!chargingRoll)
        {
            gambler_anim.Play("larry_charge");
            chargeTimer = Random.Range(reactionTimeLow,reactionTimeHigh);
            chargingRoll = true;
        }
        else
        {
            chargeTimer -= 1 * Time.deltaTime;
            if (chargeTimer <= 0)
            {
                chargeTimer = 0;
                chargingRoll = false;
                
                return true;
            }
        }

        return false;
    }

    public override void swipeDice()
    {
        
        gambler_anim.Play("larry_swipedDice");
    }

    public override void missedSwipe()
    {
        gambler_anim.Play("larry_missedSwipe");
    }
    public override void angered()
    {
        gambler_anim.Play("larry_angered");
    }

    public override void talk()
    {

    }
}
