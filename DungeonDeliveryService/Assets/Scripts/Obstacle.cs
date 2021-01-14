using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ResettableMechanic
{
    [Header("Obstacle")]
    [Tooltip("The exact weight number where obstacle triggers")]
    [SerializeField] protected int weightTrigger = 1;
    [Tooltip("What Range is the obstacle a danger")]
    [SerializeField] protected DangerTime criteria = DangerTime.exactly;

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

    // Triggers the obstacle if the player's weight matches the criteria
    //(It's used by event delegate, so it should trigger whenever player weight changes)
    protected virtual void UpdateObstacle()
    {
        if (getWeightToTrigger())
            TriggerObstacle();
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if (getWeightToTrigger())
                TriggerObstacle();
        }
    } // Close OnCollisionEnter2D
    protected virtual void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            if(getWeightToTrigger())
                TriggerObstacle();
        }
    }

    /**
     * Function checks to see if the player's weight matches the obstacle's criteria
     * returns true if weight matches criteria false otherwise
     */
    public virtual bool getWeightToTrigger()
    {
        if (criteria == DangerTime.lessThanOrEqual)
        {
            if (Player.Get.getWeight() > weightTrigger)
                return false;
        }
        else if (criteria == DangerTime.greaterThanOrEqual)
        {
            if (Player.Get.getWeight() < weightTrigger)
                return false;
        }
        else if (criteria == DangerTime.exactly)
        {
            if (Player.Get.getWeight() != weightTrigger)
                return false;
        }

        return true;
    }
    //Triggers the mechanism of the obstacle
    public virtual void TriggerObstacle()
    {
            
    }

} // close Obstacle
