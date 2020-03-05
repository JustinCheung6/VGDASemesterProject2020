using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Obstacle
{
    public override void TriggerObstacle(Player player)
    {
        player.onPlayerDeath();
        this.gameObject.SetActive(false);
    }
}
