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
    [HideInInspector]
    public bool checking = false;
    [HideInInspector]
    public bool patrolling = false;

    public PoliceProfile policeProfile;

    float dormantTimer;

    float checkTimer;

    float gameOverResetTimer;
    public float gameResetTime;

    // Start is called before the first frame update
    void Start()
    {
        currnetState = dormantState;

        gameOverResetTimer = gameResetTime;
        dormantTimer = policeProfile.dormantTime;
    }

    //set && get state
    public policeState getState() { return currnetState; }
    public void setState(policeState nextState) { currnetState = nextState;  }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GameRunning()) return;

        if (!gameManager.IsPractice() && !DialogueScript.IsRunning())
        {
            currnetState.doState(this);     
        }
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
            dormantTimer = policeProfile.dormantTime;
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
            soundManager.Instance.playPoliceAlerted();
            Debug.Log("YOU LOSE!!!!");
            currnetState.changeState(this,alertedState);
        }
        else if (checkTimer <= 0 && checking && player.hidingGame)
        {
            //sus check passed. reset sus stat
            checkTimer = 0;
            checking = false;
            //gameObject.active = false;
            policeProfile.sus = 0;
            player.resetAnger();
            Debug.Log("SAFE");
            currnetState.changeState(this,dormantState);
        }

            
    }

    internal void Hide()
    {
        police_anim.Play("police_dormant");
        door_anim.Play("door_closed");
    }

    internal void DisplayChecking()
    {
        door_anim.Play("door_open");
        police_anim.Play("police_checking");
    }

    internal void DisplayAlterted()
    {
        door_anim.Play("door_open");
        police_anim.Play("police_alerted");
    }

    public void runResetTimer()
    {
        gameOverResetTimer -= 1 * Time.deltaTime;
        Debug.Log("checkTime: " + gameOverResetTimer);
        if (gameOverResetTimer <= 0)
        {
            gameManager.OnLose();
        }
    }

    public void nothingSus()
    {
        Debug.Log("hhmmst");
    }

    public void checkBar()
    {
        float verdict = Random.Range(policeProfile.sus, policeProfile.maxSus);
        if (verdict > policeProfile.susThreshold)
        {
            checking = true;
            checkTimer = policeProfile.checkTime;
            currnetState.changeState(this,checkingState);

            door_anim.Play("door_open");
            police_anim.Play("police_checking");
            Debug.Log("SECURITY CHECK!!!");
        }
    }
}
