using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//states for the game
/*
class Context
{
    // A reference to the current state of the Context.
    private State _state = null;

    public Context(State state)
    {
        this.TransitionTo(state);
    }

    // The Context allows changing the State object at runtime.
    public void TransitionTo(State state)
    {
        Debug.Log("next state" + state);
        this._state = state;
        this._state.SetContext(this);
    }

}

abstract class State
{
    protected Context _context;

    public void SetContext(Context context)
    {
        this._context = context;
    }

    public abstract void switchState();


}

// Concrete States implement various behaviors, associated with a state of
// the Context.
class roll_phase : State
{
    public override void switchState()
    {
        //Console.WriteLine("ConcreteStateA handles request1.");
        //Console.WriteLine("ConcreteStateA wants to change the state of the context.");
        this._context.TransitionTo(new swipe_phase());
    }

}

class swipe_phase : State
{
    public override void switchState()
    {
        _context.TransitionTo(new roll_phase());
       // Console.Write("ConcreteStateB handles request1.");
    }

}
*/

public class gameTable : MonoBehaviour
{
    //manager for the dice game
    public gambler currentOpponent;
    public playerControl p_obj;

    int gameReset = 10;
    int currentTime = 0;
    int diceTotal = 0;

    bool roll_phase = false;
    bool swipe_phase = false;

    bool resetMatch =false;


   
    // Start is called before the first frame update
    void Start()
    {
        roll_phase = true;    
    }

    // Update is called once per frame
    void Update()
    {
        if (roll_phase)
        {
            //roll phase
            
        }
        else if (swipe_phase)
        {
            //swipe phase
            
        }
    }



    public void triggerReset()
    {
        resetMatch = true;

    }
}
