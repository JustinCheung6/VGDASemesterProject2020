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
                Debug.Log("Its in pit, the override trigger");
                TriggerObstacle();
            }
            else
            {
                Debug.Log("Its onPlat, no trigger");
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
                Debug.Log("Its in pit, the stay trigger");
                TriggerObstacle();
            }
            else
            {
                Debug.Log("Its onPlat, no trigger");
            }
        }
    }

    public override void TriggerObstacle()
    {
        Debug.Log("Trigger in pit, player will die");
        player.onPlayerDeath();
    }
    
}
