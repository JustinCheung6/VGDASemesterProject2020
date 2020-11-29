using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class StoryManager : FungusManager
{
    [SerializeField] private TextAsset[] scripts;
    //private Dictionary<string, Character> speakers2 = new Dictionary<string, Character>();
    [SerializeField] private List<Character> speakers = new List<Character>();

    //[SerializeField] private Dictionary<string, Block> blocks2 = new Dictionary<string, Block>();
    [SerializeField] private List<Block> blocks = new List<Block>();

    [SerializeField] private List<string> scriptNames = new List<string>();

    /*
    private void Start()
    {
        StartCoroutine(PlayScript());
    }
    private IEnumerator PlayScript()
    {
        if (blocks.Count == 0)
            Debug.Log("We did it Reddit");

        yield return new WaitForSeconds(2f); 
        if (blocks[0] != null)
            blocks[0].StartExecution();
        else
            Debug.Log("Something is wrong");
    }

    [ContextMenu("Reset Scripts")]
    private void ResetScripts()
    {
        //reset blocks
        foreach (var component in gameObject.GetComponents<Component>())
        {
            if (component.GetType() != typeof(Transform) &&
                component.GetType() != typeof(Flowchart) &&
                component.GetType() != typeof(StoryManager))
                DestroyImmediate(component);

        }

        foreach (Character_Fungus speaker in GetComponentsInChildren<Character_Fungus>())
            DestroyImmediate(speaker.gameObject);

        blocks = new Dictionary<string, Block>();
        scriptNames = new List<string>();
        speakers = new Dictionary<string, Character>();
    }

    */
    [ContextMenu("Update Scripts")]
    private void SetScripts()
    {
        //reset blocks
        foreach (var component in gameObject.GetComponents<Component>())
        {
            if (component.GetType() != typeof(Transform) &&
                component.GetType() != typeof(Flowchart) &&
                component.GetType() != typeof(StoryManager))
                DestroyImmediate(component);

        }

        foreach (Character_Fungus speaker in GetComponentsInChildren<Character_Fungus>())
            DestroyImmediate(speaker.gameObject);

        blocks = new List<Block>();
        scriptNames = new List<string>();
        speakers = new List<Character>();

        foreach (TextAsset script in scripts)
        {
            List<string> scriptLines = new List<string>();
            scriptLines.AddRange(script.text.Split("\n"[0]));

            Debug.Log(scriptLines[0]+ "a");

            Block block = CreateBlock(scriptLines[0]);
            blocks.Insert(FindScriptNameIndex(scriptLines[0]).Value, block);
            scriptNames.Add(block.BlockName);

            Character speaker = null;
            for(int i = 1; i < scriptLines.Count; i++)
            {
                if(!string.IsNullOrWhiteSpace(scriptLines[i]))
                {
                    if (scriptLines[i].Length >= 4 && scriptLines[i].Substring(0, 2).Equals("::"))
                    {
                        if (scriptLines[i].Substring(2, scriptLines[i].Length - 5) != "Null")
                        {
                            if (FindScriptNameIndex(scriptLines[i].Substring(2, scriptLines[i].Length - 5)) != null &&
                                speakers[FindScriptNameIndex(scriptLines[i].Substring(2, scriptLines[i].Length - 5)).Value] != null)
                                speaker = speakers[FindScriptNameIndex(scriptLines[i].Substring(2, scriptLines[i].Length - 5)).Value];
                            else
                            {
                                //Debug.Log("Speaker " + scriptLines[i].Substring(2, scriptLines[i].Length - 5) + " not found");

                                //Create new Speaker/Character using name given
                                GameObject newSpeaker = new GameObject(scriptLines[i].Substring(2, scriptLines[i].Length - 5));
                                newSpeaker.transform.parent = this.transform;
                                newSpeaker.AddComponent<Character_Fungus>();
                                newSpeaker.GetComponent<Character_Fungus>().SetNameText(scriptLines[i].Substring(2, scriptLines[i].Length - 5));
                                speakers.Insert(FindScriptNameIndex(scriptLines[i].Substring(2, scriptLines[i].Length - 5)).Value, 
                                    newSpeaker.GetComponent<Character_Fungus>());

                                //Set new Speaker
                                speaker = newSpeaker.GetComponent<Character_Fungus>();
                            }
                        }
                        else
                            speaker = null;
                    }
                    else
                    {
                        AddC_Say(block, scriptLines[i], speaker);
                    }
                }
            }
        }
    }

    private int? FindScriptNameIndex(string name)
    {
        for(int i = 0; i < scriptNames.Count; i++)
        {
            if (name == scriptNames[i])
                return i;
        }

        return null;
    }
}
