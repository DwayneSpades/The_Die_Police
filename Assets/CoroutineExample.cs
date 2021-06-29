using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UntilRoutine());
    }

    bool CheckInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    IEnumerator UntilRoutine()
    {
        print("Start of until");

        yield return new WaitUntil(CheckInput);

        print("you pressed something");
    }

    IEnumerator ExampleRoutine()
    {
        print("Start of the Coroutine");

        float time = 5;
        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        print("End of the Coroutine");
    }
}
