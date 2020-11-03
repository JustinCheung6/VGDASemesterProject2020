using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Obstacle
{
    [Tooltip("The door connected to the pressure plate")]
    [SerializeField] private Door door;
    [Tooltip("Whether or not the mechanism is active")]
    [SerializeField] private BoxCollider2D BC;

    protected override void Awake()
    {
        base.Awake();
        criteria = DangerTime.greaterThanOrEqual;
        weightTrigger = 1;
    }

    protected override void OnTriggerEnter2D(Collider2D c)
    {
        base.OnTriggerEnter2D(c);
    }

    //Activates whatever mechanism
    public override void TriggerObstacle()
    {
        door.OpenDoor();
        BC.enabled = false;
    }
}
