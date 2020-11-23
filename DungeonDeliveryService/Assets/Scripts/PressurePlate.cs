using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PressurePlate : Obstacle
{
    [Tooltip("The mechanism connected to the pressure plate")]
    [SerializeField] private Mechanism mechanism;
    [Tooltip("Whether or not the mechanism is active")]
    [SerializeField] private TilemapCollider2D TC;

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
        mechanism.Activate();
        TC.enabled = false;
    }
}
