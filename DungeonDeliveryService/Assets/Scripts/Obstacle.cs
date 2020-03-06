using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected int weightTrigger = 1;
    [Tooltip("What Range is the obstacle safe in")]
    [SerializeField] protected DangerTime criteria = DangerTime.exactly;
    bool activateObstacle = false;

    //Conditions for obstacle to be active (be dangerous)
    protected enum DangerTime
    {
        lessThanOrEqual = -1,
        exactly = 0,
        greaterThanOrEqual = 1
    }

    /**
     * Function to set activate Obstacle to true.
     */
    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<Player>() != null)
        {
            if (getWeightToTrigger(col.gameObject.GetComponent<Player>()))
                TriggerObstacle(col.gameObject.GetComponent<Player>());
        }
    } // Close OnCollisionEnter2D
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>() != null)
        {
            if(getWeightToTrigger(col.gameObject.GetComponent<Player>()))
                TriggerObstacle(col.gameObject.GetComponent<Player>());
        }
    }

    /**
     * Function checks to see if weight criteria matches
     */
    public virtual bool getWeightToTrigger(Player player)
    {
        if (criteria == DangerTime.lessThanOrEqual)
        {
            if (player.getWeight() > weightTrigger)
                return false;
        }
        else if (criteria == DangerTime.greaterThanOrEqual)
        {
            if (player.getWeight() < weightTrigger)
                return false;
        }
        else if (criteria == DangerTime.exactly)
        {
            if (player.getWeight() != weightTrigger)
                return false;
        }

        return true;
    }
    //Triggers the mechanism of the obstacle
    public virtual void TriggerObstacle(Player player)
    {

    }

} // close Obstacle
