using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steve : gamblerInterface
{
    float currentReaction;
    bool playedFeint;
    bool runningSwipe;
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

    // Update is called once per frame
    void Update()
    {
        if (!playedFeint && runningSwipe && gameManager.GetDiceCount() > 1 && chargeTimer < currentReaction - 1.0f)
        {
            if (gameManager.Instance.giveRollTotal() <= 6)
            {
                gambler_anim.Play("steve_feint");
            }
        }
    }

    public override void playThemeSong()
    {
        soundManager.Instance.playSteveTheme();
    }

    public override bool chargeRoll()
    {
        if (!chargingRoll)
        {
            gambler_anim.Play("steve_charge");
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
        gambler_anim.Play("steve_roll");

        Vector2 tmp_v = new Vector2(2, 0);
        Instantiate(die, tmp_v, Quaternion.identity);
    }

    public override bool runSwipeTimer()
    {
        if (!chargingRoll)
        {
            runningSwipe = true;
            gambler_anim.Play("steve_charge");
            chargeTimer = Random.Range(reactionTimeLow, reactionTimeHigh);
            currentReaction = chargeTimer;
            chargingRoll = true;
        }
        else
        {
            chargeTimer -= 1 * Time.deltaTime;
            if (chargeTimer <= 0)
            {
                chargeTimer = 0;
                chargingRoll = false;
                runningSwipe = false;
                playedFeint = false;
                return true;
            }
        }

        return false;
    }

    public override void swipeDice()
    {
        gambler_anim.Play("steve_swipedDice");
    }

    public override void missedSwipe()
    {
        gambler_anim.Play("steve_missedSwipe");
    }
    public override void angered()
    {
        gambler_anim.Play("steve_angered");
    }

    public override void talk()
    {

    }
}
