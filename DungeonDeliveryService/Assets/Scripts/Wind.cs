using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Obstacle
{
    [SerializeField] private AreaEffector2D wind;
    [SerializeField] private BoxCollider2D windCollider;
    private bool windy = false;

    private void Update()
    {
        if (getWeightToTrigger(FindObjectOfType<Player>()))
            TriggerObstacle(FindObjectOfType<Player>());
        else
            DisableTrigger(FindObjectOfType<Player>());
    }

    public override void TriggerObstacle(Player player)
    {
        windCollider.enabled = true;
        windy = true;
    }
    public void DisableTrigger(Player player)
    {
        windCollider.enabled = false;
        windy = false;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(windy)
            if (col.GetComponent<PlayerMovement>() != null)
                col.GetComponent<PlayerMovement>().StartWind((int)wind.forceAngle / 90);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<PlayerMovement>() != null)
            col.GetComponent<PlayerMovement>().StopWind((int)wind.forceAngle / 90);
    }
}
