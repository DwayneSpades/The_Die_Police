using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gambler : MonoBehaviour
{

    public int money_score = 0;
    public int rollChance = 3;
    public bool swpied = false;

    // Start is called before the first frame update
    void Start()
    {
        money_score = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addMoney(int amount)
    {
        money_score = money_score + amount;
    }

    void loseMoney(int amount)
    {
        money_score = money_score - amount;
    }

}
