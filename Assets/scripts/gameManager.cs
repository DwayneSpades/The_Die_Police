using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class gameManager : MonoBehaviour
{
    public static gameManager Instance { get; private set; }

    public readonly StandardDiceRule standardDiceRule = new StandardDiceRule();
    public readonly SnakeEyesDiceRule snakeEyesDiceRule = new SnakeEyesDiceRule();

    public LevelData[] levels;
    int currentLevel;

    private void Awake()
    {
        if (Instance == null)
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentLevel = startingLevel;
        levels[currentLevel].gambler.gameObject.SetActive(true);
        gamblerMoneyDisplay.gambler = levels[currentLevel].gambler;
    }

    [HideInInspector]
    public int diceTotal;
    [HideInInspector]
    public List<dice> diceRoll = new List<dice>();

    [Header("References")]
    public moneyDisplay gamblerMoneyDisplay;
    public police policeman;

    //name the levelsscenes 
    [Header("Scenes")]
    public string startMenu;
    public string loseScreen;
    public string winScreen;

    [Header("Debug")]
    public int startingLevel = 0;

    public void progressLevel()
    {
        //increimante level
        bool lastLevel = (currentLevel == levels.Length - 1);

        if (lastLevel)
        {
            // do winscreen stuff
            print("haha u won");
            levels[currentLevel].gambler.gameObject.SetActive(false);
        }
        else
        {
            // progress level
            levels[currentLevel].gambler.gameObject.SetActive(false);
            currentLevel++;

            levels[currentLevel].gambler.gameObject.SetActive(true);
            policeman.policeProfile = levels[currentLevel].policeProfile;
            gamblerMoneyDisplay.gambler = levels[currentLevel].gambler;
        }
    }
    
    public void collectDice(dice die)
    {
        diceTotal += die.dice_number;
       // Debug.Log("total dice points: " + diceTotal);
        diceRoll.Add(die);
    }

    public void resetDiceRoll()
    {
        for(int i=0; i < diceRoll.Count; i++)
        {
            diceRoll[i].destroyDice(); ;
        }
        diceTotal = 0;
        diceRoll.Clear();
    }

    public int giveRollTotal()
    {
       // Debug.Log("total dice points: " + Instance.diceTotal);
        return Instance.diceTotal;
    }

    public static int GetDiceCount() => Instance.diceRoll.Count;

    public static gamblerInterface GetCurrentOpponent() => Instance.levels[Instance.currentLevel].gambler;

    internal bool CheckSwipe()
    {
        bool result = standardDiceRule.Evaluate(diceRoll);
        if (result)
        {
            result = levels[currentLevel].gambler.EvaluateDiceRules(diceRoll);
        }

        return result;
    }
}
