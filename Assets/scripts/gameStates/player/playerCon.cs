using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCon : MonoBehaviour
{
    static playerCon inst;

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

    //the player's stats
    public gamblerInterface player;

    //assets on player
    // [hide]
    
    public Animator hand_anim;

    //player status
    //[SerializeField]
    float dicePower;

    //cover the game board
    [SerializeField]
    GameObject cup;

    [HideInInspector]
    public bool hidingGame = false;

   

    //get state
    public playerState getState() { return currentState; }

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cup.active = false;
    }

    // Start is called before the first frame update
    void Start()
    {

        //load starting state
        currentState = rollState;
        cup.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueScript.IsRunning()) return;
        if (!gameManager.GameRunning()) return;

        //hide game at anytime
        if (Input.GetKeyDown(KeyCode.Z))
        {
            cup.active = true;
            hidingGame = true;
            hand_anim.Play("idle");

            if (!policePatrol.checking)
            {
                //angers opponenet when police aren't around
                if (!gameManager.IsPractice())
                {
                    gameManager.GetCurrentOpponent().angered();
                    gameManager.GetCurrentOpponent().addAnger();
                }
                if (!gameManager.IsWaitingForHide())
                {
                    DialogueScript.RunDialogue(gameManager.GetCurrentLevel().hideLoseDialogue);
                }
            }

            clearTable();
        }
        //reset game at anytime
        else if (cup.active && !Input.GetKey(KeyCode.Z))
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
        bool tmp = gameManager.GetCurrentOpponent().chargeRoll();
        //Vector2 tmp_v = new Vector2(3, 0);
        //Instantiate(die, tmp_v, Quaternion.identity);
        if(tmp)
            currentState = swipeState;
    }

    internal static bool IsHiding()
    {
        return inst.cup.active;
    }

    bool charging = false;

    public void chargeDice()
    {
        if (gameManager.IsWaitingForHide()) return;

        if (Input.GetKeyDown(KeyCode.X))
        {
            charging = true;
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
        if (charging && !Input.GetKey(KeyCode.X))
        {
            hand_anim.Play("throw_dice");
            currentState = enemyRoleState;
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
    float roundTime = 2;
    bool roundOver = false;

    public void swipeDice()
    {
        if (gameManager.IsWaitingForHide()) return;

        //play swip ainmation
        bool successfulSwipe = gameManager.Instance.CheckSwipe();
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            hand_anim.Play("swipe_dice");
            playerGotDice = true;

            if (successfulSwipe)
            {
                Debug.Log("won Round!");

                if (!gameManager.IsPractice())
                {
                    player.addMoney(gameManager.GetCurrentOpponent().BettingAmount);
                    gameManager.GetCurrentOpponent().loseMoney(gameManager.GetCurrentOpponent().BettingAmount);
                }

                DialogueScript.RunDialogue(gameManager.GetCurrentLevel().loseDialogue);

                Debug.Log("PLAYER mONEY:" + player.money_score);
                roundOver = true;
            }
            else
            {
                wasBelow = true;
                Debug.Log("lost Round!");

                if (!gameManager.IsPractice())
                {
                    player.loseMoney(gameManager.GetCurrentOpponent().BettingAmount);
                    gameManager.GetCurrentOpponent().addMoney(gameManager.GetCurrentOpponent().BettingAmount);
                }

                DialogueScript.RunDialogue(gameManager.GetCurrentLevel().winDialogue);

                roundOver = true;
            }

            clearTable();
        }

        
        if (!enemyGotDice)
        {
            //run enemy swipe timer
            bool tmp = gameManager.GetCurrentOpponent().runSwipeTimer();

            if (tmp && !playerGotDice)
            {
                if (successfulSwipe)
                {
                    gameManager.GetCurrentOpponent().swipeDice();

                    if (!gameManager.IsPractice())
                    {
                        player.loseMoney(gameManager.GetCurrentOpponent().BettingAmount);
                        gameManager.GetCurrentOpponent().addMoney(gameManager.GetCurrentOpponent().BettingAmount);
                    }
                    roundOver = true;

                    DialogueScript.RunDialogue(gameManager.GetCurrentLevel().winDialogue);
                }
                else
                {
                    gameManager.GetCurrentOpponent().missedSwipe();

                    if (!gameManager.IsPractice())
                    {
                        player.addMoney(gameManager.GetCurrentOpponent().BettingAmount);
                        gameManager.GetCurrentOpponent().loseMoney(gameManager.GetCurrentOpponent().BettingAmount);
                    }
                    roundOver = true;

                    DialogueScript.RunDialogue(gameManager.GetCurrentLevel().loseDialogue);
                }


                clearTable();
                enemyGotDice = true;

            }
            else if (tmp && playerGotDice)
            {
                //if the oppnenet is too late to swipe
                if (wasBelow)
                {
                    gameManager.GetCurrentOpponent().swipeDice();
                    DialogueScript.RunDialogue(gameManager.GetCurrentLevel().winDialogue);
                }
                else
                {
                    gameManager.GetCurrentOpponent().missedSwipe();
                    DialogueScript.RunDialogue(gameManager.GetCurrentLevel().loseDialogue);
                }
                enemyGotDice = true;
            }
            
        }
        if (roundOver)
        {
            roundTimer -= 1 * Time.deltaTime;

            if(roundTimer <= 0)
            {
                roundOver = false;
                currentState = resetState;
            }
        }

    }
    /*
    int resetTimer = 0;
    int resetTime = 3;
    */

    public void resetAnger()
    {
        //reset current opponents anger
        gameManager.GetCurrentOpponent().resetAnger();
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
        else if (gameManager.GetCurrentOpponent().money_score <= 0)
        {
            gameManager.Instance.progressLevel();
        }


        playerGotDice = false;
        //restart match
        currentState = rollState;
    }

}
