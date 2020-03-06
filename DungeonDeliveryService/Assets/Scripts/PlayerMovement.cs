using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Set user moveSpeed for DeliveryPerson.
    public float moveSpeed = 5.0f;
    // Get the Object for the DeliveryPerson
    public Rigidbody2D rb;
    public float acceleration = 1.0f;
    // Set up the movement variables that will be called.
    float moveHorizontal, moveVertical;

    // Checks if direction is disabled because of wind: 0 = Left, 1 = Down, 2 = Right, 3 = Up
    [SerializeField]private bool[] wind = { false, false, false, false };

    public void StartWind(int index)
    {
        wind[index] = true;
    }
    public void StopWind(int index)
    {
        wind[index] = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2 currentPosition = transform.position;
    } //Close Start

    // Update is called once per frame
    void Update()
    {
        // Get the respective Horizontal and Vertical movement.
        moveHorizontal = Input.GetAxis("Horizontal");

        moveVertical = Input.GetAxis("Vertical");
    } // Close Update

    private void FixedUpdate()
    {


        // Calculate the player's net movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Set movement to zero if player is going against wind direction
        if (movement.x < 0 && wind[0])
            movement.x = 0;
        if (movement.x > 0 && wind[2])
            movement.x = 0;

        if (movement.y < 0 && wind[1])
            movement.y = 0;
        if (movement.y > 0 && wind[3])
            movement.y = 0;

        // Apply the actual movement
        transform.position = ((Vector2) transform.position) + (movement * moveSpeed * Time.fixedDeltaTime * 10f);
    } //Close FixedUpdate
} //Close PlayerMovement
