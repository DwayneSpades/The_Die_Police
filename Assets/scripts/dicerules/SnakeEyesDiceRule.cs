using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEyesDiceRule : DiceRule
{
    public override bool Evaluate(List<dice> dices)
    {
        bool initialized = false;
        int value = 0;

        foreach(var die in dices)
        {
            if (!initialized)
            {
                initialized = true;
                value = die.dice_number;
            }
            else
            {
                if (die.dice_number == value)
                {
                    // snake eyes! you can't pick this up.
                    return false;
                }
            }
        }

        // no duplicates found
        return true;
    }
}
