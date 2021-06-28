using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkDie : dice
{
    [HideInInspector]
    public int fake_number;

    public Sprite[] pinkSprites;
    public float reresolve_time = 0.5f;
    public float flip_time = 0.5f;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        fake_number = Random.Range(0, 6) + 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (revealTime > 0)
        {
            revealTime -= Time.deltaTime;
            if (revealTime <= 0)
            {
                animator.enabled = false;
                GetComponent<SpriteRenderer>().sprite = pinkSprites[fake_number - 1];
            }
        }
        else
        {
            if (reresolve_time > 0)
            {
                reresolve_time -= Time.deltaTime;
                if (reresolve_time <= 0)
                {
                    animator.enabled = true;
                }
            }
            else
            {
                if (flip_time > 0)
                {
                    flip_time -= Time.deltaTime;
                    if (flip_time <= 0)
                    {
                        animator.enabled = false;
                        GetComponent<SpriteRenderer>().sprite = dieSprites[dice_number - 1];
                    }
                }
            }
        }

    }

}
