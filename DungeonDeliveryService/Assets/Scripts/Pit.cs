using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : Obstacle
{
    public override void TriggerObstacle()
    {
        player.onPlayerDeath();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
