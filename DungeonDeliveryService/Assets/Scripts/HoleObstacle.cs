using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleObstacle : MonoBehaviour
{

    public GameObject block;
    public GameObject hole;
    public Collider2D holeCol;

    private void Start()
    {
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

        if (col.gameObject.CompareTag("Block"))
        {
            // code to be executed on collision

            //if (hole.gameObject.CompareTag("Hole"))
            //{
            //holeCol.isTrigger = true;
            //}

            //holeCol.isTrigger = true;

            // col.isTrigger = true;
            // hole.GetComponent<Collider2D>().enabled = true;

            // Trying to enable trigger for hole when player touched to make the hole 
            // as a "blocked path" but won't work. Then once the block collides with
            // the hole, the hole collider should be disabled and player should pass
            // through, but still not working. Is that even possible to do? Any help is appreciated.

            //if (hole.gameObject.CompareTag("Block"))
            //{
            //holeCol.enabled = false;
            //}

            //holeCol.enabled = false;
            col.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            col.gameObject.SetActive(false);
            hole.SetActive(false);

            // block.isStatic = true;
            // sets the rigidbody of the block from dynamic to static
            
            // block.GetComponent<Renderer>().enabled = false;
            /*
             * disables gameobject while also letting the block gameobject be visible,
               by disabiling the block collider. it's a good way to set gameobjects 
               as inactive while letting them be visible at the same time.
             */
            //col.enabled = false;
        }
    }
}
