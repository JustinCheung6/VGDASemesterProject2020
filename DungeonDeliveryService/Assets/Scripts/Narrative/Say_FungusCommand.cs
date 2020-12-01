using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Say_FungusCommand : Say
{
    public Character Character
    {
        get { return character; }
        set { character = value; }
    }

    public string StoryText
    {
        get { return storyText; }
        set { storyText = value; }
    }

    public string Descrption
    {
        get { return description; }
        set { description = value; }
    }
    public bool ExtendPrevious
    {
        get { return extendPrevious; }
        set { extendPrevious = value; }
    }

    public AudioClip VoiceOverClip
    {
        get { return voiceOverClip; }
        set { voiceOverClip = value; }
    }

    public bool ShowAlways
    {
        get { return showAlways; }
        set { showAlways = value; }
    }
    public bool FadeWhenDone
    {
        get { return fadeWhenDone; }
        set { fadeWhenDone = value; }
    }
    public bool WaitForClick
    {
        get { return waitForClick; }
        set { waitForClick = value; }
    }
    public bool StopVoiceOver
    {
        get { return stopVoiceover; }
        set { stopVoiceover = value; }
    }
    public SayDialog SetSayDialog
    {
        get { return setSayDialog; }
        set { setSayDialog = value; }
    }
    public bool WaitForVO
    {
        get { return waitForVO; }
        set { waitForVO = value; }
    }
}
