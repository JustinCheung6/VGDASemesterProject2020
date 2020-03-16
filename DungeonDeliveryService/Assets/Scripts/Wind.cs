using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Obstacle
{
    [SerializeField] private AreaEffector2D wind;
    [SerializeField] private BoxCollider2D windCollider;
    private bool strongWinds = false;
    private bool playerBlocked = false;

    public bool StrongWinds { get => strongWinds; }
    protected override void Awake()
    {
        base.Awake();

        if (wind == null)
            wind = GetComponent<AreaEffector2D>();
        if (windCollider == null)
            windCollider = GetComponent<BoxCollider2D>();
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
            wind.enabled = true;
        else
            wind.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!playerBlocked)
            if (col.CompareTag("Player"))
                if(getWeightToTrigger() != strongWinds)
                {
                    strongWinds = !strongWinds;
                    TriggerObstacle();
                }
    }
}
