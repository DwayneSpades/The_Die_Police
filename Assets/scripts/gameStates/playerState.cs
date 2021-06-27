using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface playerState
{
    //To get to the swipe state both players need to have rolled thier dice
    playerState doState(playerCon player);
    //To get back to the roll state both players need to swipe
    

}


public class introPhasee : playerState
{
    //To get to the swipe state both players need to have rolled thier dice
    public playerState doState(playerCon player)
    {
        //do what the state does
        //player.rollEnemyDice();


        //tansition to next state
        return player.getState();
    }
    //To get back to the roll state both players need to swipe


}

public class outroPhasee : playerState
{
    //To get to the swipe state both players need to have rolled thier dice
    public playerState doState(playerCon player)
    {
        //do what the state does
        //player.rollEnemyDice();


        //tansition to next state
        return player.getState();
    }
    //To get back to the roll state both players need to swipe


}


public class enemyRollPhase : playerState
{
    //To get to the swipe state both players need to have rolled thier dice
    public playerState doState(playerCon player)
    {
        //do what the state does
        player.rollEnemyDice();


        //tansition to next state
        return player.getState();
    }
    //To get back to the roll state both players need to swipe


}

public class rollPhase : playerState
{
    //To get to the swipe state both players need to have rolled thier dice
    public playerState doState(playerCon player)
    {
        //do what the state does
        player.chargeDice();


        //tansition to next state
        return player.getState();
    }
    //To get back to the roll state both players need to swipe


}

public class swipePhase : playerState
{
    //To get to the swipe state both players need to have rolled thier dice
    public playerState doState(playerCon player)
    {
        player.swipeDice();

        //tansition to next state
        return player.getState();
    }
    //To get back to the roll state both players need to swipe
}

public class resetPhase : playerState
{
    //To get to the swipe state both players need to have rolled thier dice
    public playerState doState(playerCon player)
    {
        player.resetMatch();

        //tansition to next state
        return player.getState();
    }
    //To get back to the roll state both players need to swipe


}