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
    public bool CheckNarrativeProgress(string scriptName)
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

    private Scenes currentScene = Scenes.Cutscene;

    //Audio
    private AudioSource audio = null;
    [SerializeField] private AudioClip beginningSong;
    [SerializeField] private AudioClip finalStretch;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else if(singleton != this)
        {
            Debug.Log("ProgressManager Duplicate Found");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        audio = GetComponentInChildren<AudioSource>();

        audio.clip = beginningSong;

        GoToScene(Scenes.Cutscene);
    }

    public void GoToScene(Scenes scenes)
    {
        if(scenes == Scenes.Cutscene)
        {
            audio.Stop();
            PlayerMovement.singleton.PlayAnimation = true;
            StoryManager.Get.PlayScene(CurrentScript);
        }

        if(scenes == Scenes.Game)
        {
            audio.Play();
            PlayerMovement.singleton.PlayAnimation = false;
        }

        //SceneManager.LoadScene((int)scenes);
    }
}
