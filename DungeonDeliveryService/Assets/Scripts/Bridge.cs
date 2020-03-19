using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Obstacle
{
    //This is to get rid of delegate as the bridge script doesn't need it
    protected override void OnEnable()
    {
        
    }
    protected override void OnDisable()
    {
        
    }
    //Straight up kill player if On collision and disable object (make bridge disappear
    public override void TriggerObstacle()
    {
        player.onPlayerDeath();
        this.gameObject.SetActive(false);
    }
}
