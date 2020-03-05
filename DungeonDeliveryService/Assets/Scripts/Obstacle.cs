using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected int weightTrigger = 1;
    [Tooltip("What Range is the obstacle safe in")]
    [SerializeField] protected Criteria criteria = Criteria.exactly;
    bool activateObstacle = false;

    protected enum Criteria
    {
        lessThan = -1,
        exactly = 0,
        greaterThan = 1

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
        if (criteria == Criteria.lessThan)
        {
            if (player.getWeight() > weightTrigger)
                return false;
        }
        else if (criteria == Criteria.greaterThan)
        {
            if (player.getWeight() < weightTrigger)
                return false;
        }
        else if (criteria == Criteria.exactly)
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
