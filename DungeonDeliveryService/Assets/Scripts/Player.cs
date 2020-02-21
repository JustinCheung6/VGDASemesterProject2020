using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Set the intital weight of the player to 0.
    private int weight = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    } // Close Start

    // Update is called once per frame
    void Update()
    {
        
    } // Close Update

    /**
     * Function called when player dies.
     */
    public void onPlayerDeath()
    {
        //Save the current scene to return to after death.
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        // There should be a "load a death scene" that plays when the player dies but we don't have that, so we'll immediately just restart the level instead.

        // Reload the current scene due to death. Yes, it uses the above value, for futureproofing if there's ever a "death screen". 
        // Tbh, we may end up changing the flow later on anyways.
        SceneManager.LoadScene(currentScene);

    } //Close onDeath

    /**
     * increaseWeight function that increases weight by 1.
     */
    public void increaseWeight()
    {
        this.weight += 1;
    } //Close increaseWeight

    /**
    * decreaseWeight function that decreases weight by 1.
    */
    public void decreaseWeight()
    {
        this.weight -= 1;
    } //Close decreaseWeight

    /**
     * Function to return weight of player.
     */
     public int getWeight()
    {
        return this.weight;
    } // Close getWeight

} //Close Player
