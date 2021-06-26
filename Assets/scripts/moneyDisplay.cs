using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class moneyDisplay : MonoBehaviour
{
    public gambler gambler_stats;
    public Text textDisplay;

    // Start is called before the first frame update
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
