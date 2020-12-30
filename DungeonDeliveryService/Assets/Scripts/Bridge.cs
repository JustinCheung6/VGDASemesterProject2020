using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bridge : Obstacle
{
    private TilemapRenderer tilemapRenderer = null;

    private string quipName = "PitRQuip";

    private void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    //Straight up kill player if On collision and disable object (make bridge disappear)
    public override void TriggerObstacle()
    {
        StartCoroutine(Player.Get.onPlayerDeath(1f));
        StartCoroutine(RespawnBridge());
    }

    private IEnumerator RespawnBridge()
    {
        tilemapRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        StoryManager.Get.PlayQuip(quipName);
        yield return new WaitForSeconds(0.5f);
        tilemapRenderer.enabled = true;
        
    }
}
