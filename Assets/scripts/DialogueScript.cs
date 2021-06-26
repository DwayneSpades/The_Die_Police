using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    static DialogueScript inst;

    [Header("Speed Fields")]
    public float normalCharDelay = 0.1f;
    public float fastCharDelay = 0.05f;

    WaitForSeconds waitNormal;
    WaitForSeconds waitFast;

    // pointer to swap between waitNormal and waitFast
    WaitForSeconds wait;

    // yield for input
    WaitUntil waitUntilInput;

    [Header("Hookups")]
    public TextMeshProUGUI dialogueText;
    public GameObject panel;
    public Image continueIcon;

    bool isRunning = false;

    Dialogue currentDialogue;

    [Header("Keycodes to listen for")]
    public KeyCode[] keycodes;

    bool speedup = false;

    [Header("Leave null to disable.")]
    public Dialogue test;

    void Awake()
    {
        // singleton pattern
        if (inst == null) {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // allocating these once only
        waitNormal = new WaitForSeconds(normalCharDelay);
        waitFast = new WaitForSeconds(fastCharDelay);
        waitUntilInput = new WaitUntil(() =>
        {
            return CheckButtons();
        });

        panel.SetActive(false);

        if (test)
        {
            DialogueScript.RunDialogue(test, () => { print("test is done"); });
        }
    }

    // Action callback is a function you can pass in like so: DialogueScript.RunDialogue(dialogue, () => {print("hello");})
    public static void RunDialogue(Dialogue dialogue, Action callback = null)
    {
        if (!inst.isRunning)
        {
            inst.currentDialogue = dialogue;
            inst.StartCoroutine(inst.RunRoutine(callback));
        }
    }

    public static bool IsRunning() => inst.isRunning;

    void Update()
    {
        if (wait == waitNormal)
        {
            if (CheckButtons())
            {
                wait = waitFast;
            }
        }
    }

    // internal methods ------

    bool CheckButtons()
    {
        foreach (var kc in keycodes)
        {
            if (Input.GetKey(kc))
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator RunRoutine(Action callback)
    {
        panel.SetActive(true);
        continueIcon.enabled = false;

        isRunning = true;
        foreach(var str in currentDialogue.dialogue)
        {
            wait = waitNormal;

            dialogueText.text = str;
            dialogueText.maxVisibleCharacters = 0;

            // text reveal effect
            foreach (var c in str)
            {
                dialogueText.maxVisibleCharacters = dialogueText.maxVisibleCharacters + 1;
                yield return wait;
            }

            // wait for button press
            continueIcon.enabled = true;
            yield return waitUntilInput;
        }

        panel.SetActive(false);
        isRunning = false;

        // ?. is syntax for nullable types
        callback?.Invoke();
    }

}
