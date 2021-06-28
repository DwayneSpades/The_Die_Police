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

    bool practice = false;
    bool waitingForHide = false;

    public Animator fadeAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        dialogueCallback = () =>
        {
            if (currentLevel == 0)
            {
                switch (currentDialogue)
                {
                    case 0:
                        // start basic practice, run police 
                        StartCoroutine(Practice());
                        break;
                    case 1:
                        // wait for player to hide with Z
                        StartCoroutine(WaitForHide());
                        break;
                    case 2:
                        // start game
                        practice = false;
                        break;
                }

                currentDialogue++;
            }
            else
            {
                currentDialogue++;
                if (currentDialogue < levels[currentLevel].introDialogues.Length)
                {
                    DialogueScript.RunDialogue(levels[currentLevel].introDialogues[currentDialogue], dialogueCallback);
                }
            }
        };

        currentLevel = startingLevel;
        
        StartLevel();
    }

    internal static bool GameRunning()
    {
        return Instance.gameRunning;
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

    int currentDialogue = 0;

    bool gameRunning = true;

    IEnumerator Practice()
    {
        practice = true;
        float time = 0;
        while (time < 15)
        {
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitUntil(() => { return !DialogueScript.IsRunning(); });

        policeman.DisplayChecking();

        DialogueScript.RunDialogue(levels[currentLevel].introDialogues[currentDialogue], dialogueCallback);
    }

    IEnumerator WaitForHide()
    {
        waitingForHide = true;

        yield return new WaitUntil(() =>
        {
            return !DialogueScript.IsRunning() && playerCon.IsHiding();
        });

        waitingForHide = false;
        practice = false;
        policeman.Hide();
        DialogueScript.RunDialogue(levels[currentLevel].introDialogues[currentDialogue], dialogueCallback);
    }

    Action dialogueCallback = () => { };

    void StartLevel()
    {
        currentDialogue = 0;

        levels[currentLevel].gambler.gameObject.SetActive(true);
        //play level song
        levels[currentLevel].gambler.playThemeSong();

        policeman.policeProfile = levels[currentLevel].policeProfile;
        gamblerMoneyDisplay.gambler = levels[currentLevel].gambler;

        DialogueScript.RunDialogue(levels[currentLevel].introDialogues[currentDialogue], dialogueCallback);
    }

    public void progressLevel()
    {
        //increimante level
        bool lastLevel = (currentLevel == levels.Length - 1);

        if (lastLevel)
        {
            // do winscreen stuff
            levels[currentLevel].gambler.gameObject.SetActive(false);
            gameManager.OnLose();
        }
        else
        {
            // progress level
            StartCoroutine(EndLevel());
        }
    }

    WaitUntil dialogueWait = new WaitUntil(() => { return !DialogueScript.IsRunning(); });

    IEnumerator EndLevel()
    {
        gameRunning = false;
        foreach(var dialogue in GetCurrentLevel().outtroDialogues)
        {
            DialogueScript.RunDialogue(dialogue, () =>
            {
                if (dialogue.displayPolice)
                {
                    policeman.DisplayAlterted();
                }
            });

            yield return dialogueWait;
        }
        fadeAnimator.Play("FadeOut");

        float time = 0;
        while (time < 2.0f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        policeman.Hide();
        levels[currentLevel].gambler.gameObject.SetActive(false);
        currentLevel++;
        levels[currentLevel].gambler.gameObject.SetActive(true);

        fadeAnimator.Play("FadeIn");

        time = 0;
        while (time < 2.0f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        gameRunning = true;
        StartLevel();
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
    public static LevelData GetCurrentLevel() => Instance.levels[Instance.currentLevel];

    internal bool CheckSwipe()
    {
        bool result = standardDiceRule.Evaluate(diceRoll);
        if (result)
        {
            result = levels[currentLevel].gambler.EvaluateDiceRules(diceRoll);
        }

        return result;
    }

    public static bool IsPractice() => Instance.practice;
    public static bool IsWaitingForHide() => Instance.waitingForHide;

    IEnumerator OnLoseRoutine()
    {
        gameRunning = false;

        fadeAnimator.Play("FadeOut");
        float time = 0; 
        while (time < 2)
        {
            time += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void OnLose() => Instance.StartCoroutine(Instance.OnLoseRoutine());

}
