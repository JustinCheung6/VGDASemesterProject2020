using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : ResettableMechanic
{
    [Tooltip("Use extremely large mass for pushing illusion")]
    //[SerializeField] Rigidbody2D RB;
    [SerializeField] float waitTIme = 3f;

    [SerializeField] private float moveSpeed = 1f;
    private bool moving = false;
    private bool colliding = false;
    private Vector2 finalPos = new Vector2();
    private Vector2 velocity = new Vector2();

    private Renderer sprite = null;
    private Collider2D col = null;

    private void Start()
    {
        sprite = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position = transform.position + (Vector3)velocity * Time.fixedDeltaTime;

            if( (velocity.x > 0 && transform.position.x >= finalPos.x) || (velocity.y > 0 && transform.position.y >= finalPos.y) || 
                (velocity.x < 0 && transform.position.x <= finalPos.x) || (velocity.y < 0 && transform.position.y <= finalPos.y) )
            {
                //Debug.Log("Destination");
                transform.position = finalPos;
                velocity = new Vector2();
                finalPos = new Vector2();
                moving = false;
            }
        }
    }

    //Changes mass to simulate player pushing block "inch-by-inch", flag to indicate finished simulation
    IEnumerator PushBlock(int xPushing = 0, int yPushing = 0, float xTime = 0, float yTime = 0)
    {
        xPushing = (Input.GetAxisRaw("Horizontal") == xPushing || xPushing == 0) ? (int)Input.GetAxisRaw("Horizontal") : 0;
        yPushing = (Input.GetAxisRaw("Vertical") == yPushing || yPushing == 0) ? (int)Input.GetAxisRaw("Vertical") : 0;
        xTime = (xPushing != 0) ? xTime + 0.1f : 0;
        yTime = (yPushing != 0) ? yTime + 0.1f : 0;

        yield return new WaitForSeconds(0.1f);

        //Debug.Log("xTime " + xTime + " yTime " + yTime);

        if ((xTime >= waitTIme || yTime >= waitTIme) && !moving)
        {
            //Debug.Log("Pushed");
            finalPos = (Vector2)transform.position + transform.localScale * new Vector2((xTime >= waitTIme) ? xPushing : 0, (yTime >= waitTIme) ? yPushing : 0);
            velocity = new Vector2(xPushing, yPushing) * moveSpeed;

            moving = true;

            yTime = xTime = 0;
            xPushing = yPushing = 0;
        }
        if(colliding)
            StartCoroutine(PushBlock(xPushing, yPushing, xTime, yTime));
    }

    protected override void MechanicReset()
    {
        col.enabled = true;
        sprite.enabled = true;
        base.MechanicReset();
    }

    //Player collision detection
    protected void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Player") && !moving)
        {
            StartCoroutine(PushBlock());
            colliding = true;
        }
    }
    public void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            StopCoroutine(PushBlock());
            colliding = false;
        }
    }

}
