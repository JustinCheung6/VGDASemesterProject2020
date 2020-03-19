using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Event that all obstacles are going to connect to. Activates when weight changes
    public delegate void UpdateWorld();
    public static event UpdateWorld WeightChanged;

    // Set the intital weight of the player to 0.
    [SerializeField] private int weight = 0;
    
    // Variable for most recent level.
    private int currentScene;

    /**
     * Function called when player dies.
     */
    public void onPlayerDeath()
    {

        currentScene = SceneManager.GetActiveScene().buildIndex;

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
        
        //Updates all obstacles that the player weight has changed.
        WeightChanged?.Invoke();

    } //Close increaseWeight

    /**
    * decreaseWeight function that decreases weight by 1.
    */
    public void decreaseWeight()
    {
        this.weight -= 1;

        //Updates all obstacles that the player weight has changed.
        WeightChanged?.Invoke();

    } //Close decreaseWeight

    /**
     * Function to return weight of player.
     */
     public int getWeight()
    {
        return this.weight;
    } // Close getWeight

    //This function invokes Weightchanged event delegate. Debug use only
    [ContextMenu("Update Weight")]
    private void updateWeightDebug()
    {
        WeightChanged?.Invoke();
    }

} //Close Player
