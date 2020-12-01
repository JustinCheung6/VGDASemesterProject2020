using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Wait_FungusCommand : Wait
{
    public float Duration
    {
        get { return _duration; }
        set { _duration = new FloatData(value); }
    }
}
