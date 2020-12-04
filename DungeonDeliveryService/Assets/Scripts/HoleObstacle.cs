using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleObstacle : ResettableMechanic
{
    private SpriteRenderer sprite = null;
    private Collider2D col = null;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        // Trying to enable trigger for hole when player touched to make the hole 
        // as a "blocked path" but won't work. Any help is appreciated.
        //if (hole.gameObject.CompareTag("Player"))
        //{
        //holeCol.isTrigger = false;

        //holeCol.enabled = true;
        //}
    }

    private void Update()
    {
       
    }

    // if block collides with hole, block will fill hole and 
    // the player can proceed 
    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.GetComponent<MovableBlock>() != null)
        {
            col.gameObject.GetComponent<Renderer>().enabled = false;
            col.gameObject.GetComponent<Collider2D>().enabled = false;
            sprite.enabled = false;
            this.col.enabled = false;
        }
    }

    protected override void MechanicReset()
    {
        sprite.enabled = true;
        col.enabled = true;
        base.MechanicReset();
    }
}
