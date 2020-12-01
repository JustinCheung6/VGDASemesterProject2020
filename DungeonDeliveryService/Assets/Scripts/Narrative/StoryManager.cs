using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class StoryManager : MonoBehaviour
{
    Dictionary<int, Block> narrativeBeats;

    private void Start()
    {
        GetComponent<Flowchart>().ExecuteIfHasBlock("EndingNarrative");
    }
}
