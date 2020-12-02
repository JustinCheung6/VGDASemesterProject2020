using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class StoryManager : MonoBehaviour
{
    private Flowchart flowchart = null;

    private void Start()
    {
        flowchart = GetComponent<Flowchart>();

        if(ProgressManager.Get != null)
        {
            flowchart.StopAllBlocks();
            flowchart.ExecuteIfHasBlock(ProgressManager.Get.CurrentScript);
        }
        StartCoroutine(ReturnToGame());
    }

    private IEnumerator ReturnToGame()
    {
        yield return new WaitUntil(() => !flowchart.HasExecutingBlocks());
        yield return new WaitForSeconds(1);

        ProgressManager.Get.GoToScene(ProgressManager.Scenes.Game);
    }
}
