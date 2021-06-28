using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a field under the Create Menu (right click in project and select "Create")
[CreateAssetMenu(fileName = "Dialogue", menuName = "DiePolice/Dialogue")]
public class Dialogue : ScriptableObject
{
    // Makes the typing area bigg
    [TextArea(3, 100)]
    public string[] dialogue;

    public bool displayPolice;
    public bool hidePolice;
    public Sprite gamblerSprite;
}
