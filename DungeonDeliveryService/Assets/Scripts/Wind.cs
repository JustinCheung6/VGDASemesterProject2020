using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Obstacle
{
    [SerializeField] private AreaEffector2D wind;
    [SerializeField] private BoxCollider2D windCollider;
    private bool strongWinds = false;
    private bool playerBlocked = false;

    private bool playerRestricted = false;
    private PlayerMovement playerMove = null;

    public bool StrongWinds { get => strongWinds; }
    protected override void Awake()
    {
        base.Awake();

        if (wind == null && GetComponent<AreaEffector2D>() != null)
            wind = GetComponent<AreaEffector2D>();
        if (windCollider == null && GetComponent<BoxCollider2D>() != null)
            windCollider = GetComponent<BoxCollider2D>();
        if (playerMove == null && player.GetComponent<PlayerMovement>() != null)
            playerMove = player.GetComponent<PlayerMovement>();
    }

    protected override void UpdateObstacle()
    {
        if (getWeightToTrigger() != strongWinds)
        {
            strongWinds = !strongWinds;
            TriggerObstacle();
        }
    }

    public void ToggleBlocked(bool blocked)
    {
        if (playerBlocked == blocked)
            return;

        playerBlocked = blocked;

        if (strongWinds)
            TriggerObstacle();
    }

    public override void TriggerObstacle()
    {
        if (strongWinds && !playerBlocked)
        {
            wind.enabled = true;

            if (!playerRestricted && playerMove != null)
            {
                playerRestricted = true;
                playerMove.AddRestrictions((int)wind.forceAngle / 90);

            }
        }
        else
        {
            wind.enabled = false;
            if (playerRestricted)
            {
                playerRestricted = false;
                playerMove.RemoveRestrictions((int)wind.forceAngle / 90);
            }
        }
    }

}
