using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// everything is public in here becuase this is acting as a struct
// unity and C# do weird things with structs so this is for safety
[System.Serializable]
public class PoliceProfile
{
    public float sus = 0;
    public float maxSus = 0;
    public float susThreshold = 50;

    public float dormantTime=3;

    public float checkTime=3;
}
