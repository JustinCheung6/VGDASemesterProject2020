using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFloor : MonoBehaviour
{
    // Set user speed for DeliveryPerson.
    public float speed = 5.0f;
    // Set slide speed for DeliveryPerson
    //public float slideSpeed = 4.75f;
    // Set torque for DeliveryPerson
    public float torque = 1.0f;
    // The type of force used
    //ForceMode2D m_ForceMode = ForceMode2D.Force;
    // Get the Object for the DeliveryPerson
    public Rigidbody2D rb;
    //public float accel = 1.0f;
    // Set up the movement variables that will be called.
    private float moveH, moveV;
    [SerializeField] private bool aniMovement = false;
    //Check if player is moving by the controller
    private bool cMovement = false;

    public bool IsMoving { get => (aniMovement || cMovement); }

    // Checks if direction is disabled because of wind: 0 = Left, 1 = Down, 2 = Right, 3 = Up
    [SerializeField] private int[] restrictions = { 0, 0, 0, 0 };

    //Stops player movement at a specfic direction
    public void AddRestrictions(int index)
    {
        restrictions[index] += 1;
    }
    //Stops player movement altogether
    public void AddRestrictions()
    {
        rb.velocity = Vector3.zero;

        for (int i = 0; i < restrictions.Length; i++)
        {
            restrictions[i] += 1;
        }
    }
    //Removes a restrction from player movement at a specific direction
    public void RemoveRestrictions(int index)
    {
        restrictions[index] -= 1;
    }
    //Removes a restrction from player movement in all directions
    public void RemoveRestrictions()
    {
        for (int i = 0; i < restrictions.Length; i++)
        {
            restrictions[i] -= 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2 currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!aniMovement)
        {
            // Get the respective Horizontal and Vertical movement.
            moveH = Input.GetAxis("Horizontal");
            moveV = Input.GetAxis("Vertical");
        }
    }

    private void FixedUpdate()
    {
        // Calculate the player's net movement.
        Vector2 movement = new Vector2(moveH, moveV);

        //GetComponent<Rigidbody2D>().AddForce(movement * speed * Time.deltaTime);
        //GetComponent<Rigidbody2D>().AddTorque(Vector2.up * torque * moveH);
        //GetComponent<Rigidbody2D>().AddTorque(torque * moveH);
        //rb.AddForce(movement * speed * Time.deltaTime);
        //rb.AddTorque(torque * moveH);
        //rb.AddTorque(torque * moveV);

        if (!aniMovement)
        {

            //Check if player is moving at all
            if (movement == Vector2.zero)
            {
                cMovement = false;
            }
            else
            {
                cMovement = true;
            }

            // Apply the actual movement
            //transform.position = ((Vector2)transform.position) + (movement * speed * Time.fixedDeltaTime * 10f);
        }

        /*
         * if player collides with the ice floor, the player won't be able to move in another direction
         * while on the ice. Alternatively, OnTriggerStay can also be used.
         * 
         * */
        void OnTriggerEnter2D(Collider2D collision)
    {
            GetComponent<Collider2D>().sharedMaterial.friction = 0.1f;

            if (collision.gameObject.CompareTag("Player"))
            {
                AddRestrictions(0);
                AddRestrictions(2);
                rb.AddForce(movement * speed * Time.deltaTime);
                rb.AddTorque(torque * moveH);
                rb.AddTorque(torque * moveV);

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

                // Apply the actual movement
                transform.position = ((Vector2)transform.position) + (movement * speed * Time.fixedDeltaTime * 10f);

                //if (movement == Vector2.zero)
                //{
                //GetComponent<Collider2D>().sharedMaterial.friction = 0.1f;
                //}

                //movement = Vector2.zero;
                //GetComponent<Collider2D>().sharedMaterial.friction = 0.1f;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                RemoveRestrictions(0);
                RemoveRestrictions(2);
            }
        }
  } // Close FixedUpdate
}
