using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected Player player;

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
    protected virtual void Awake()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    
    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if (getWeightToTrigger())
                TriggerObstacle();
        }
    } // Close OnCollisionEnter2D
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(getWeightToTrigger())
                TriggerObstacle();
        }
    }

    /**
     * Function checks to see if weight criteria matches
     */
    public virtual bool getWeightToTrigger()
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
    public virtual void TriggerObstacle()
    {

    }

} // close Obstacle
