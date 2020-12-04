using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class StoryManager : MonoBehaviour
{
    private Flowchart flowchart = null;
    public static StoryManager singleton = null;
    public static StoryManager Get { get => singleton; }


    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(this.gameObject);
    }
    private void Start()
    {
        flowchart = GetComponent<Flowchart>();
    }

    public void PlayScene(string scriptName)
    {
        CameraManager.Get.AnimcationCamera(true);

        if (ProgressManager.Get != null)
        {
            flowchart.StopAllBlocks();
            flowchart.ExecuteIfHasBlock(scriptName);
        }
        StartCoroutine(ReturnToGame());
    }

    private IEnumerator ReturnToGame()
    {
        yield return new WaitUntil(() => !flowchart.HasExecutingBlocks());
        yield return new WaitForSeconds(1);

        ProgressManager.Get.GoToScene(ProgressManager.Scenes.Game);
        CameraManager.Get.AnimcationCamera(false);
    }
    public void PlayQuip(string quipName, bool needsResponse = false)
    {
        if (needsResponse)
            PlayerMovement.singleton.PlayAnimation = true;

        flowchart.StopAllBlocks();
        flowchart.ExecuteIfHasBlock(quipName);

        if(needsResponse)
            StartCoroutine(MoveDelay());
    }

    private IEnumerator MoveDelay()
    {
        yield return new WaitUntil(() => !flowchart.HasExecutingBlocks());
        PlayerMovement.singleton.PlayAnimation = false;
    }

}
