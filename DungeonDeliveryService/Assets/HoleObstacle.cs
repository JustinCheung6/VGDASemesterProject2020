using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleObstacle : MonoBehaviour
{

    public GameObject block;
    public GameObject hole;

    // if block collides with hole, block will fill hole and 
    // the player can proceed 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Block"))
        {
            //code to be executed on collision
            hole.SetActive(false);
            //block.isStatic = true;
            //sets the rigidbody of the block from dynamic to static
            block.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //block.SetActive(false);
            //block.GetComponent<Renderer>().enabled = false;
            /*
             * disables gameobject while also letting the block gameobject be visible,
               by disabiling the block collider. it's a good way to set gameobjects 
               as inactive while letting them be visible at the same time.
             */
            col.enabled = false;
        }
    }
}
