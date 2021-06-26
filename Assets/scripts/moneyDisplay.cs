using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class moneyDisplay : MonoBehaviour
{
    public GameObject gambler_stats;
    public Text textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay.text = "money: " + gambler_stats.GetComponent<gambler>().money_score;
    }

    // Update is called once per frame
    void Update()
    {
        textDisplay.text = "money: " + gambler_stats.GetComponent<gambler>().money_score;
    }
}
