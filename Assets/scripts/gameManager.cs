using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{


    private gameManager() { }

    private static gameManager _instance;

    public static gameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new gameManager();
        }
        return _instance;
    }


    int currentLevel = 1;


    int playerMoney = 0;
    int opponentMoney = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void setMoneyAmount(int player_m,int enemy_m)
    {
        playerMoney = player_m;
        opponentMoney = enemy_m; 
    }
}
