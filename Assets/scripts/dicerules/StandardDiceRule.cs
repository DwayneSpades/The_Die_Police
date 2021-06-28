using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardDiceRule : DiceRule
{
    public override bool Evaluate(List<dice> dices)
    {
        int diceSum = 0;
        foreach(var die in dices)
        {
            diceSum += die.dice_number;
        }

        return diceSum > 6;
    }
}
