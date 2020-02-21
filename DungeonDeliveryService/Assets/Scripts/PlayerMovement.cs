﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        
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

        // Apply the actual movement
        rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime * 10f);
    } //Close FixedUpdate
} //Close PlayerMovement
