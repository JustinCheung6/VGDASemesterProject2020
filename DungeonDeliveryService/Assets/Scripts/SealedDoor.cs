using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SealedDoor : Mechanism
{
    [SerializeField] protected Collider2D col;
    [SerializeField] private SpriteRenderer ren;

    //Sprites
    [SerializeField] private Sprite doorClosed = null;
    [SerializeField] private Sprite doorOpen = null;

    //Activate method for the sealed door

    private void Start()
    {
        if (col == null)
            col = GetComponent<Collider2D>();
        if (ren == null)
            ren = GetComponent<SpriteRenderer>();
        ren.sprite = doorClosed;
    }

    public override void Activate()
    {

        ren.sprite = doorOpen;
        col.enabled = false;
    }

    //Deactivate method for the sealed door
    public override void Deactivate()
    {
        ren.sprite = doorClosed;
        col.enabled = true;
    }
}
