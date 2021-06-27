using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dice : MonoBehaviour
{
    public int dice_number;
    public List<Sprite> dienumber;
    // Start is called before the first frame update
    void Awake()
    {
        dice_number = Random.Range(0, 6);
        GetComponent<SpriteRenderer>().sprite = dienumber[dice_number];
        

        dice_number += 1;
        Debug.Log("dice rolled: " + dice_number);

        gameManager.Instance.collectDice(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyDice()
    {
        Destroy(gameObject);
    }

}
