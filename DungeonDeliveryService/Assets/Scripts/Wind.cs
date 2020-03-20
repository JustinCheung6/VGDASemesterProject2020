using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Obstacle
{
    //Is true when player's weight is in range of obstacle (decides if player is restricted)
    private bool strongWinds = false;
    public bool StrongWinds { get => strongWinds; } //Property of strongWinds
    //Is true when player is behind a windblock object (decides if player is restricted)
    private bool playerBlocked = false;
    //Is true when the wind is blowing against the players (restricts player from walking towards wind)
    private bool playerRestricted = false;

    //Object References
    private PlayerMovement playerMove = null;
    private AreaEffector2D wind;
    private BoxCollider2D windCollider;

    //Obstacle gets updated everytime player weight changes
    private void OnEnable()
    {
        Player.WeightChanged += UpdateObstacle;
    }
    private void OnDisable()
    {
        Player.WeightChanged -= UpdateObstacle;
    }
    protected override void Awake()
    {
        base.Awake();
        //Reference objects
        if (wind == null)
            wind = GetComponent<AreaEffector2D>();
        if (windCollider == null)
            windCollider = GetComponent<BoxCollider2D>();
        if (playerMove == null)
            playerMove = player.GetComponent<PlayerMovement>();
    }

    //Checks player weight to see if strongWind should activate
    protected override void UpdateObstacle()
    {
        if (getWeightToTrigger() != strongWinds)
        {
            strongWinds = !strongWinds;
            //Checks to see if AreaEffector should activate
            TriggerObstacle();
        }
    }
    //Changes playerBlocked to the value of the blocked param
    public void ToggleBlocked(bool blocked)
    {
        //Stops script if values already matches
        if (playerBlocked == blocked)
            return;

        playerBlocked = blocked;

        //Checks to see if AreaEffector should activate
        TriggerObstacle();
    }
    //Activates AreaEffector and player Restriciton if criteria is met
    public override void TriggerObstacle()
    {
        if (strongWinds && !playerBlocked)
        {
            wind.enabled = true;

            //playerRestricted makes sure this script activates AddRestrictions only once
            if (!playerRestricted && playerMove != null)
            {
                playerRestricted = true;
                playerMove.AddRestrictions((int)wind.forceAngle / 90);

            }
        }
        else
        {
            wind.enabled = false;
            //playerRestricted makes sure this script activates RemoveRestrictions only once
            if (playerRestricted)
            {
                playerRestricted = false;
                playerMove.RemoveRestrictions((int)wind.forceAngle / 90);
            }
        }
    }

}
