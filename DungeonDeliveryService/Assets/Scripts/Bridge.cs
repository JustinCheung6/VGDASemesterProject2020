using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Obstacle
{

    //Straight up kill player if On collision and disable object (make bridge disappear)
    public override void TriggerObstacle()
    {
        player.onPlayerDeath();
        this.gameObject.SetActive(false);
    }
}
