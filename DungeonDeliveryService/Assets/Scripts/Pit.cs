using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pit : MonoBehaviour
{
    private static int platform = 0;
    public bool Platform {
        get => platform > 0;
        set
        {
            platform += (value) ? 1 : -1;
            if (platform < 0) platform = 0;
        }
    }
    protected void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            if(!Platform)
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
            if(!Platform)
            {
                Debug.Log("stay: Move.onPlat == false");
                TriggerObstacle();
            }
        }
    }

    public void TriggerObstacle()
    {
        Debug.Log("Trigger in pit, player will die");
        StartCoroutine(Player.Get.onPlayerDeath());
    }
    
}
