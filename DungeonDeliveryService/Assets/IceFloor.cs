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
    public float accel = 1.0f;
    // Set up the movement variables that will be called.
    private float moveH, moveV;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the respective Horizontal and Vertical movement.
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

    } // Close Update

    private void FixedUpdate()
    {
        // Calculate the player's net movement.
        Vector2 movement = new Vector2(moveH, moveV);

        //GetComponent<Rigidbody2D>().AddForce(movement * speed * Time.deltaTime);
        //GetComponent<Rigidbody2D>().AddTorque(Vector2.up * torque * moveH);
        //GetComponent<Rigidbody2D>().AddTorque(torque * moveH);
        rb.AddForce(movement * speed * Time.deltaTime);
        rb.AddTorque(torque * moveH);
        rb.AddTorque(torque * moveV);

        // if player collides with the ice floor, the player won't be able to move in another direction
        // while on the ice
        void OnTriggerStay2D(Collider2D collision)
    {
            if (collision.gameObject.CompareTag("Player"))
            {

                if(movement == Vector2.zero)
                {
                    GetComponent<Collider2D>().sharedMaterial.friction = 0.1f;
                }

                //movement = Vector2.zero;
                //GetComponent<Collider2D>().sharedMaterial.friction = 0.1f;
            }
    }
  } // Close FixedUpdate
}
