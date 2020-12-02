using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    private static ProgressManager singleton;
    public static ProgressManager Get { get=> singleton; }

    //Narrative
    private List<string> narrativeProgress = new List<string>();
    private bool CheckNarrativeProgress(string scriptName)
    {
        if (narrativeProgress.Contains(scriptName))
            return true;
        return false;
    }

    [SerializeField] private const string STARTINGSCRIPT = "OpeningNarrative";
    private string currentScript = "";
    public string CurrentScript
    {
        get { 
            string script = currentScript;
            if (narrativeProgress.Count == 0)
                script = STARTINGSCRIPT;
            narrativeProgress.Add(script);
            currentScript = null; 
            return script; 
        }
        set { currentScript = value; }
    }

    //Scenes
    [SerializeField] public enum Scenes
    {
        Title = 0,
        Cutscene = 1,
        Game = 2
    }

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(singleton != this)
        {
            Debug.Log("ProgressManager Duplicate Found");
            Destroy(this.gameObject);
        }
    }

    public void GoToScene(Scenes scenes)
    {
        SceneManager.LoadScene((int)scenes);
    }
}
