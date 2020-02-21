using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int weightTrigger = 1;
    Rigidbody2D obstacleCollision;
    Player deliveryPerson;
    bool activateObstacle = false;

    // Start is called before the first frame update
    void Start()
    {
        
    } // close Start

    // Update is called once per frame
    void Update()
    {
        
    } // close Update

    /**
     * Function to set activate Obstacle to true.
     */
    public void OnCollisionEnter2D(Collision collision)
    {
        if(deliveryPerson.getWeight() > weightTrigger)
        {
            activateObstacle = true;
        }
    } // Close OnCollisionEnter2D

    /**
     * Function to return the int of the triggerWeight.
     */
    public int getWeightToTrigger()
    {
        return this.weightTrigger;
    } // Close getWeightToTrigger

} // close Obstacle
