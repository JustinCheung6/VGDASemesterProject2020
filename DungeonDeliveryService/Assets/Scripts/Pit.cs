using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pit : Obstacle
{
    public bool platform;
    protected override void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            if(platform==false)
            {
                Debug.Log(" enter: Move.onPlat == false");
                TriggerObstacle();
            }
        }
    }

    protected void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            if(platform==false)
            {
                Debug.Log("stay: Move.onPlat == false");
                TriggerObstacle();
            }
        }
    }

    public override void TriggerObstacle()
    {
        Debug.Log("Trigger in pit, player will die");
        player.onPlayerDeath();
    }
    
}
