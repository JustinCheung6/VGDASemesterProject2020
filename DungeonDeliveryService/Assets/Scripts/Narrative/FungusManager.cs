using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FungusManager : MonoBehaviour
{

    [SerializeField] protected Flowchart flowchart;
    private int idCounter = 0;

    public Block CreateBlock(string blockName)
    {
        //Create block component and add to gameObject
        Block block = flowchart.gameObject.AddComponent<Block>();
        block.BlockName = blockName;
        block.ItemId = idCounter++;

        return block;
    }

    public void AddEH_GameStarted(Block block, int delayFrames = 1)
    {
        if (block == null)
        {
            Debug.Log("Tried to add event handler to empty block: GameStarted");
            return;
        }

        GameStarted_FungusEH eventAdded = block.gameObject.AddComponent<GameStarted_FungusEH>();
        eventAdded.WaitForFrames = delayFrames;

        block._EventHandler = (GameStarted)eventAdded;
    }

    public void AddEH_MessageRecieved(Block block, string message)
    {
        if (block == null)
        {
            Debug.Log("Tried to add event handler to empty block: GameStarted");
            return;
        }

        MessageReceived_FungusEH eventAdded = block.gameObject.AddComponent<MessageReceived_FungusEH>();
        eventAdded.Message = message;

        block._EventHandler = (MessageReceived) eventAdded;
    }

    public void AddC_Say(Block block, string storyText, Character character = null, AudioClip voClip = null, bool showAlways = true, 
        bool fade = true, bool waitClick = true, bool waitForVO = false, bool extendPrev = false, SayDialog setSayDialog = null, string description = "")
    {
        if (block == null)
        {
            Debug.Log("Tried to add command to empty block: Say");
            return;
        }

        Say_FungusCommand command = block.gameObject.AddComponent<Say_FungusCommand>();

        command.StoryText = storyText;
        command.Character = character;
        command.VoiceOverClip = voClip;
        command.ShowAlways = showAlways;
        command.FadeWhenDone = fade;
        command.WaitForClick = waitClick;
        command.StopVoiceOver = !waitForVO;
        command.WaitForVO = waitForVO;
        command.ExtendPrevious = extendPrev;
        command.SetSayDialog = setSayDialog;
        command.Descrption = description;

        block.CommandList.Add(command);
    }

    public void AddC_Wait(Block block, float duration = 1)
    {
        if (block == null)
        {
            Debug.Log("Tried to add command to empty block: Wait");
            return;
        }

        Wait command = block.gameObject.AddComponent<Wait>();
    }
}
