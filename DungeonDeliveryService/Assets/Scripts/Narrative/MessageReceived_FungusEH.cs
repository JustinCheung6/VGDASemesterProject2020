using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class MessageReceived_FungusEH : MessageReceived
{
    public string Message
    {
        get { return message; }
        set { message = value; }
    }
}
