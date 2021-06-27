using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class police : MonoBehaviour
{
    //gameplay states
    policeState currnetState;

    dormant dormantState = new dormant();
    checking checkingState = new checking();
    //resetPhase resetState = new resetPhase();
    alerted alertedState = new alerted();


    [SerializeField]
    Animator police_anim;

    [SerializeField]
    Animator door_anim;

    public playerCon player;
    public float sus = 0;
    public float maxSus = 0;
    public float susThreshold = 50;

    public bool checking = false;
    public bool patrolling = false;

    float dormantTimer;
    public float dormantTime;

    float checkTimer;
    public float checkTime;

    float gameOverResetTimer;
    public float gameOverResetTime;

    // Start is called before the first frame update
    void Start()
    {
        currnetState = dormantState;

        gameOverResetTimer = gameOverResetTime;
    }

    //set && get state
    public policeState getState() { return currnetState; }
    public void setState(policeState nextState) { currnetState = nextState;  }

    // Update is called once per frame
    void Update()
    {
        currnetState.doState(this);     
    }

    public void runCoolDownTimer()
    {
        dormantTimer -= 1 * Time.deltaTime;
        police_anim.Play("police_dormant");
        door_anim.Play("door_closed");
        if (dormantTimer <= 0)
        {
            checking = false;
            checkBar();
            
            Debug.Log("RANDOM CHECK");
            dormantTimer = dormantTime;
        }

    }

    public void runCheckTimer()
    {
        
        checkTimer -= 1 * Time.deltaTime;
        

        if (checkTimer <= 0 && checking && !player.hidingGame)
        {
            checkTimer = 0;
            checking = false;

            police_anim.Play("police_alerted");
            Debug.Log("YOU LOSE!!!!");
            currnetState.changeState(this,alertedState);
        }
        else if (checkTimer <= 0 && checking && player.hidingGame)
        {
            //sus check passed. reset sus stat
            checkTimer = 0;
            checking = false;
            //gameObject.active = false;
            sus = 0;
            player.resetAnger();
            Debug.Log("SAFE");
            currnetState.changeState(this,dormantState);
        }

            
    }


    public void runResetTimer()
    {
        gameOverResetTimer -= 1 * Time.deltaTime;
        Debug.Log("checkTime: " + gameOverResetTimer);
        if (gameOverResetTimer <= 0)
        {
            SceneManager.LoadScene(gameManager.Instance.loseScreen);
        }
    }

    public void nothingSus()
    {

        Debug.Log("hhmmst");
    }

    public void checkBar()
    {
        float verdict = Random.Range(sus,maxSus);
        if (verdict >susThreshold)
        {
            checking = true;
            checkTimer = checkTime;
            currnetState.changeState(this,checkingState);

            door_anim.Play("door_open");
            police_anim.Play("police_checking");
            Debug.Log("SECURITY CHECK!!!");
        }
   


    }
}
