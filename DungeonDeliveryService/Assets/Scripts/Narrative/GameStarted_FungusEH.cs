using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameStarted_FungusEH : GameStarted
{
    public int WaitForFrames
    {
        get { return waitForFrames; }
        set { waitForFrames = value; }
    }
}
