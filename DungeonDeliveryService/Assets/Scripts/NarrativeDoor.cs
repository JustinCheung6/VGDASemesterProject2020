using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeDoor : MonoBehaviour
{
    [SerializeField] private string scriptName = "";

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!ProgressManager.Get.CheckNarrativeProgress(scriptName))
            {
                col.gameObject.GetComponent<Player>().decreaseWeight();
                ProgressManager.Get.CurrentScript = scriptName;
                ProgressManager.Get.GoToScene(ProgressManager.Scenes.Cutscene);
            }
            else
            {

            }
        }
    }
}
