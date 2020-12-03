using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltIceFloor : MonoBehaviour
{
    // Player GameObject
    public GameObject p;
    // Speed of the player
    public float speed = 5.0f;
    //Movement variables
    private float moveH, moveV;
    Vector2 currentPosition;
    public Rigidbody2D rb;
    public Collider2D iceFloor;
    public Vector2 movement;
    public string id;
    public Vector2 posUpdate;
    public bool isMoving = true;

    // Checks if direction is disabled because of ice: 0 = Left, 1 = Down, 2 = Right, 3 = Up
    [SerializeField] private int[] restrictions = { 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the respective Horizontal and Vertical movement
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
    }


    private void FixedUpdate()
    {
        // The movement or direction vector of the player
        movement = new Vector2(moveH, moveV);

        if (isMoving == false)
        {
            // Updating position of the player
            posUpdate = (movement * speed * Time.deltaTime);
            // Adds constant force to the player
            p.GetComponent<PlayerMovement>().AddConstForce(id, posUpdate);
        }
        else
        {
            //Set movement to zero if movement is restricted in a specific direction
            if (movement.x < 0 && restrictions[0] > 0)
                movement.x = 0;
            else if (movement.x > 0 && restrictions[2] > 0)
                movement.x = 0;
            else
                //This prevents player from being affected by ice floor after it's out of the range
                rb.velocity = new Vector2(0, rb.velocity.y);

            if (movement.y < 0 && restrictions[1] > 0)
                movement.y = 0;
            else if (movement.y > 0 && restrictions[3] > 0)
                movement.y = 0;
            else
                //This prevents player from being affected by ice floor after it's out of the range
                rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    } // Close FixedUpdate

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = false;
            p.GetComponent<PlayerMovement>().AddRestrictions();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = true;
            // Removes constant force to the player
            p.GetComponent<PlayerMovement>().RemoveConstForce(id);

            p.GetComponent<PlayerMovement>().RemoveRestrictions();
        }
    }
}
