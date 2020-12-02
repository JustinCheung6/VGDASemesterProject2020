using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFloor : MonoBehaviour
{
    public GameObject Player;
    //Movement variables
    private float moveH, moveV;
    // Boolean variable that detects whether Player is moving
    public bool canMove = true;
    Vector2 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveInSingleDirection(movement);
        //transform.Translate(currentPosition);

        if(canMove)
        {
            // Get the respective Horizontal and Vertical movement.
            moveH = Input.GetAxis("Horizontal");
            moveV = Input.GetAxis("Vertical");
        }
    }

    void MoveInSingleDirection(Vector2 directionChange)
    {
        // Check for canMove
        if (!canMove)
            return;

        // Change direction
        currentPosition = directionChange;

        // Prevents player from moving until collision
        canMove = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            MoveInSingleDirection(currentPosition);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.CompareTag("Wall"))
        {
            // Set speed to 0
            currentPosition = Vector2.zero;
            canMove = true;
        }
    }
}
