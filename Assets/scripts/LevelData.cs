using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public PoliceProfile policeProfile;
    public gamblerInterface gambler;

    public Dialogue[] introDialogues;
    public Dialogue winDialogue;
    public Dialogue loseDialogue;
    public Dialogue hideLoseDialogue;
    public Dialogue[] outtroDialogues;
}
