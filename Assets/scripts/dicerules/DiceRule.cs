using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DiceRule
{
    // if this returns true, the roll is good. False, and you lose the bet.
    public abstract bool Evaluate(List<dice> dices);
}
