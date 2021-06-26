using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControl : MonoBehaviour
{
    public Sprite hand_throwDice;
    public Sprite hand_holdDice;

    //public GameObject playerObject;
    public gambler opponent;
    public gambler player;

    public police theFeds;
    public GameObject cloak;

    public float dicePower = 0;
    public int diceTotal;

    bool throwingDice = false;
    bool diceDown = false;
    bool swiping = false;

    public bool hideGame = false;

    public float enemyReactionTime;
    public float enemySwipeTimer;

    public int money_pot = 100;

    public float throw_animTimer;
    public float throw_animTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = hand_holdDice;
        opponent = GameObject.FindGameObjectWithTag("opponent").GetComponent<gambler>();

        cloak.active = false;

        throwingDice = false;
        hideGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        throw_animTimer -= 1 * Time.deltaTime;
        if (throw_animTimer <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = hand_holdDice;
            throw_animTimer = 0;
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            //reset the match
            throwingDice = false;
            dicePower = 0;
            opponent.anger += 1;

            hideGame = true;
            diceDown = false;
            cloak.active = true;

            //theFeds.nothingSus();

            Debug.Log("Hiding Game!!!!");
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            hideGame = false;
            cloak.active = false;
            Debug.Log("Game resumes!!!!");
        }

        // throw the dice and get a number
        if (!hideGame)
        {

            //swiping phase
            if (diceDown)
            {
                //swipe dice after they are thrown
                if (Input.GetKeyDown(KeyCode.C))
                {
                    //play hand swipe animation
                    swiping = true;
                    diceDown = false;

                    player.addMoney(money_pot);
                    opponent.loseMoney(money_pot);
                }

                //start enemy swipe countdown
                enemySwipeTimer -= 1 * Time.deltaTime;

                //match time ends here
                if (enemySwipeTimer <= 0)
                {

                    player.loseMoney(money_pot);
                    opponent.addMoney(money_pot);
          
                    //reset match
                    enemySwipeTimer = 0;
                    diceDown = false;
                }

            }
            else
            {
                //trigger dice throw
                if (Input.GetKeyDown(KeyCode.X) && !throwingDice)
                {
                    throwingDice = true;
                    Debug.Log("ROLLING DICE");
                }

                //while dice throw is being charged up
                if (throwingDice)
                {
                    dicePower += 1 * Time.deltaTime;
                }

                //throw dice
                if (Input.GetKeyUp(KeyCode.X) && throwingDice)
                {
                    GetComponent<SpriteRenderer>().sprite = hand_throwDice;
                    throw_animTimer = throw_animTime;

                    throwingDice = false;

                    Debug.Log("DICE THROWN! Power: " + dicePower);

                    diceTotal = Random.Range(2, 13);

                    Debug.Log("DICE NUMBER: " + diceTotal);

                    addSus(dicePower);
                    dicePower = 0;
                    swiping = false;
                    diceDown = true;


                    //begin swipe phase
                    enemySwipeTimer= opponent.reactionTime;
                }
            }
           
            
            
            
        }

        if (opponent.money_score <= 0)
        {
            SceneManager.LoadScene("speakEasy");
        }
        else if (player.money_score <= 0)
        {
            SceneManager.LoadScene("loseScreen");
        }
    }


    void loadNextOpponenet(gambler opponent)
    { 
        enemyReactionTime = opponent.reactionTime;
    }

    void addSus(float amount)
    {
        theFeds.sus += amount;
        theFeds.checkBar();
    }

    void resetMatch()
    {
        dicePower = 0;
        throwingDice = false;
        swiping = false;
    }

    void resetGame()
    {
        gameTable tmp = FindObjectOfType<gameTable>();
        tmp.triggerReset();

    }
}
