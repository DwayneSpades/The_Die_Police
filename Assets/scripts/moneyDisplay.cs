using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class moneyDisplay : MonoBehaviour
{
    public gamblerInterface gambler_stats;
    public Text textDisplay;
    

    //Gonna need a way to connect the current opponet to the UI
    
    void Start()
    {
        textDisplay.text = "money: " + gambler_stats.money_score;
    }

    // Update is called once per frame
    void Update()
    {
        textDisplay.text = "money: " + gambler_stats.money_score;
    }
}
