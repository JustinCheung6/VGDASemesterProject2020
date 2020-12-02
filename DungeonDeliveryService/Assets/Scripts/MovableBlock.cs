using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    [Tooltip("Use extremely large mass for pushing illusion")]
    [SerializeField] protected float mass = 10000;
    [SerializeField] Rigidbody2D RB;
    [SerializeField] protected bool done;

    //Player collision detection
    public void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ChangeMass());
        }
    }

    //Changes mass to simulate player pushing block "inch-by-inch", flag to indicate finished simulation
    IEnumerator ChangeMass()
    {
        yield return new WaitForSecondsRealtime(2);
        RB.mass = 30;
        yield return new WaitForSecondsRealtime(2);
        RB.mass = 10000;
        done = true;
    }

    //If player is still "pushing" the block, continues simulation
    public void OnCollisionStay2D(Collision2D c)
    {
        if(done)
        {
            done = false;
            if (c.gameObject.CompareTag("Player"))
            {
                StartCoroutine(ChangeMass());
            }
        }
    }

}
