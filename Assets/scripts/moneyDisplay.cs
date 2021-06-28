using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class moneyDisplay : MonoBehaviour
{
    public gamblerInterface gambler;
    public Text textDisplay;

    public string prefix;
    // Update is called once per frame
    void Update()
    {
        if (gambler)
        {
            textDisplay.text = prefix + " money: " + gambler.money_score;
        }
    }
}
