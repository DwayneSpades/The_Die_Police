using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface policeState
{
    //To get to the swipe state both players need to have rolled thier dice
    policeState doState(police popo);
    void changeState(police popo, policeState nextState);
    //To get back to the roll state both players need to swipe


}


public class dormant : policeState
{
    //To get to the swipe state both players need to have rolled thier dice
    public policeState doState(police popo)
    {
        //run the cool down timer to determine if check occurs
        popo.runCoolDownTimer();
        //tansition to next state
        return popo.getState();
    }
    //To get back to the roll state both players need to swipe
    public void changeState(police popo,policeState nextState)
    {
        popo.setState(nextState);
    }

}

public class checking : policeState
{
    //To get to the swipe state both players need to have rolled thier dice
    public policeState doState(police popo)
    {
        //run the cool down timer to determine if check occurs
        popo.runCheckTimer();

        //tansition to next state
        return popo.getState();
    }

    //To get back to the roll state both players need to swipe
    public void changeState(police popo, policeState nextState)
    {
        popo.setState(nextState);
    }
}

public class alerted : policeState
{
    //To get to the swipe state both players need to have rolled thier dice
    public policeState doState(police popo)
    {
        //run the cool down timer to determine if check occurs

        //tansition to next state
        return popo.getState();
    }

    //To get back to the roll state both players need to swipe
    public void changeState(police popo, policeState nextState)
    {
        popo.setState(nextState);
    }
}
