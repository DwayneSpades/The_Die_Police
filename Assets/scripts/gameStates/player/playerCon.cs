using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCon : MonoBehaviour
{
    //[SerializeField]
    //string currentStateName;
    playerState currentState;

    //states
    rollPhase rollState = new rollPhase();
    swipePhase swipeState = new swipePhase();
    resetPhase resetState = new resetPhase();
    enemyRollPhase enemyRoleState = new enemyRollPhase();

    //dice stuff
    [SerializeField]
    dice die;
    [SerializeField]
    police policePatrol;

    //current opponent
    public gamblerInterface currentOpponent;
    //the player's stats
    public gamblerInterface player;

    //assets on player
    public Animator hand_anim;

    //player status
    [SerializeField]
    float dicePower;

    //cover the game board
    [SerializeField]
    GameObject cup;

    [HideInInspector]
    public bool hidingGame = false;

   

    //get state
    public playerState getState() { return currentState; }

    void awake()
    {
        cup.active = false;
    }

    // Start is called before the first frame update
    void Start()
    {

        //load starting state
        currentState = enemyRoleState;
        cup.active = false;
    }

    // Update is called once per frame
    void Update()
    {

        //hide game at anytime
        if (Input.GetKeyDown(KeyCode.Z))
        {
            cup.active = true;
            hidingGame = true;
            hand_anim.Play("idle");

            if (!policePatrol.checking)
            {
                //angers opponenet when police aren't around
                currentOpponent.angered();
                currentOpponent.addAnger();
            }

            clearTable();
        }
        //reset game at anytime
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            cup.active = false;
            hidingGame = false;
            currentState = resetState;
        }

        if (!hidingGame)
        { 
            //do state action
            currentState = currentState.doState(this);
        }
    }


    public void loadNextOpponent()
    {
        //instantiate next opponent
        
    }

    public void rollEnemyDice()
    {
        //roll enemy dice
        //instantiate dice
        bool tmp = currentOpponent.chargeRoll();
        //Vector2 tmp_v = new Vector2(3, 0);
        //Instantiate(die, tmp_v, Quaternion.identity);
        if(tmp)
            currentState = rollState;
    }

    public void chargeDice()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            hand_anim.Play("charge_dice");
        }
            
        //trigger dice throw
        if (Input.GetKey(KeyCode.X))
        {
            dicePower += 1 * Time.deltaTime;
            //Debug.Log("charging DICE");
        }


        //throw dice onto the table
        //notify the gameMnaager that the player rolled
        if (Input.GetKeyUp(KeyCode.X))
        {
            hand_anim.Play("throw_dice");
            currentState = swipeState;
            roundTimer = roundTime;
            enemyGotDice = false;
            wasBelow = false;

            Vector2 tmp_v = new Vector2(3, 0);
            Instantiate(die, tmp_v, Quaternion.identity);
        }
    }
    bool playerGotDice = false;
    bool enemyGotDice = false;
    bool wasBelow = false;

    float roundTimer = 0;
    float roundTime = 8;

    public void swipeDice()
    {
        //play swip ainmation
        int diceTotal = gameManager.Instance.giveRollTotal();
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            hand_anim.Play("swipe_dice");
            playerGotDice = true;

            if (diceTotal > 6)
            {
                Debug.Log("won Round!");

                player.addMoney(currentOpponent.BettingAmount);
                currentOpponent.loseMoney(currentOpponent.BettingAmount);

                Debug.Log("PLAYER mONEY:" + player.money_score);
            }
            else
            {
                wasBelow = true;
                Debug.Log("lost Round!");

                player.loseMoney(currentOpponent.BettingAmount);
                currentOpponent.addMoney(currentOpponent.BettingAmount);
            }

            clearTable();
        }

        
        if (!enemyGotDice)
        {
            //run enemy swipe timer
            bool tmp = currentOpponent.runSwipeTimer();

            if (tmp && !playerGotDice)
            {
                if (diceTotal > 6)
                {
                    currentOpponent.swipeDice();
                    
                    player.loseMoney(currentOpponent.BettingAmount);
                    currentOpponent.addMoney(currentOpponent.BettingAmount);
                    
                }
                else
                {
                    currentOpponent.missedSwipe();

                    player.addMoney(currentOpponent.BettingAmount);
                    currentOpponent.loseMoney(currentOpponent.BettingAmount);
                }
                

                clearTable();
                enemyGotDice = true;

            }
            else if (tmp && playerGotDice)
            {
                //if the oppnenet is too late to swipe
                if (wasBelow)
                {
                    currentOpponent.swipeDice();
                }
                else
                {
                    currentOpponent.missedSwipe();
                }
                enemyGotDice = true;
            }
            
        }
        roundTimer -= 1 * Time.deltaTime;

        if(roundTimer <= 0)
        {
            currentState = resetState;
        }
       
    }
    /*
    int resetTimer = 0;
    int resetTime = 3;
    */
     
    public void resetAnger()
    {
        //reset current opponents anger
        currentOpponent.resetAnger();
    }

    public void clearTable()
    {
        gameManager.Instance.resetDiceRoll();
    }

    public void resetMatch()
    {
        //Debug.Log("reseting match");
        //play dialogue here
        // 
        // advance to next level
        // 
        // 
        // restart mattch
        // 
        // 
        //clearTable();


        if (player.money_score <= 0)
        {
            SceneManager.LoadScene(gameManager.Instance.loseScreen);
        }
        else if (currentOpponent.money_score <= 0)
        {
            gameManager.Instance.getNextLevel();
        }


        playerGotDice = false;
        //restart match
        currentState = enemyRoleState;
    }

}
