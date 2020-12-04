using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SealedDoor : Mechanism
{
    [SerializeField] protected Collider2D col;
    [SerializeField] private Renderer ren;

    //Activate method for the sealed door

    private void Start()
    {
        if (col == null)
            col = GetComponent<Collider2D>();
        if (ren == null)
            ren = GetComponent<Renderer>();
    }

    public override void Activate()
    {

        ren.enabled = false;
        col.enabled = false;
    }

    //Deactivate method for the sealed door
    public override void Deactivate()
    {
        ren.enabled = true;
        col.enabled = true;
    }
}
