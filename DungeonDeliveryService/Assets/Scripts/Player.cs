using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static Player singleton = null;
    public static Player Get { get => singleton; }

    private SpriteRenderer spriteRenderer;

    //Event that all obstacles are going to connect to. Activates when weight changes
    public delegate void UpdateWorld();
    public static event UpdateWorld WeightChanged;

    // Set the intital weight of the player to 0.
    [SerializeField] private int weight = 0;

    private Vector3 respawnPoint = new Vector3();
    public void SetRespawnPoint(Vector2 destination) { respawnPoint = destination; }

    private void OnEnable()
    {
        if(singleton == null) { singleton = this; }
        else if(singleton != this) { Debug.Log("Multiple instances of Player script found"); }
    }
    private void OnDisable() { if(singleton == this) { singleton = null; } }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() { respawnPoint = transform.position; }

    /**
     * Function called when player dies.
     */
    public IEnumerator onPlayerDeath(float delay = 1f)
    {
        spriteRenderer.enabled = false;
        PlayerMovement.singleton.AddRestrictions();
        yield return new WaitForSeconds(delay);
        transform.position = respawnPoint;
        spriteRenderer.enabled = true;
        PlayerMovement.singleton.RemoveRestrictions();

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
