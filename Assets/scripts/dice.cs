using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dice : MonoBehaviour
{
    [HideInInspector]
    public int dice_number;
    public List<Sprite> dieSprites;
    protected Animator animator;
    public float revealTime = 0.5f;
    bool hasCollided = false;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        soundManager.Instance.playThrowDice();
        dice_number = Random.Range(0, 6) + 1;
        Debug.Log("dice rolled: " + dice_number);

        gameManager.Instance.collectDice(this);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (revealTime > 0)
        {
            revealTime -= Time.deltaTime;
            if (revealTime <= 0)
            {
                animator.enabled = false;
                GetComponent<SpriteRenderer>().sprite = dieSprites[dice_number-1];
            }
        }
    }

    public void destroyDice()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if(!hasCollided)
            soundManager.Instance.playDiceImpact();
        hasCollided = true;
    }
}
