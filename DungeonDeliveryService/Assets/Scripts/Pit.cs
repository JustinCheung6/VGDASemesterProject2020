using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : Obstacle
{
    [SerializeField]private GameObject moveGO;
    private MovingPlatform move;
    void Start()
    {
        move = moveGO.GetComponent<MovingPlatform>();
    }
    protected override void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
//            TriggerObstacle();
            if(move.onPlat==false)
            {
                TriggerObstacle();
            }
        }
    }

    protected void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
//            TriggerObstacle();
            if(move.onPlat==false)
            {
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
