﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PressurePlate : Obstacle
{
    [Tooltip("The mechanism connected to the pressure plate")]
    [SerializeField] private Mechanism mechanism;
    [Tooltip("Whether or not the mechanism is active")]
    [SerializeField] private bool needsContinuous;

    protected override void Awake()
    {
        base.Awake();
        criteria = DangerTime.greaterThanOrEqual;
        weightTrigger = 1;
    }

    //Checks if object is a moving block or player object
    //player needs certain weight to trigger, blocks always trigger
    protected override void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.GetComponent<MovableBlock>())
        {
            TriggerObstacle();
        }
        else
        {
            base.OnTriggerEnter2D(c);
        }
    }

    //Disable connected mechanism if the pressure plate requires a continous force
    public void OnTriggerExit2D(Collider2D c)
    {
        if (needsContinuous)
        {
            DisableObstacle();
        }
    }

    //Activates the connected mechanism
    public override void TriggerObstacle()
    {
        mechanism.Activate();
    }

    //Deactivates the connected mechanism
    public void DisableObstacle()
    {
        mechanism.Deactivate();
    }
}
