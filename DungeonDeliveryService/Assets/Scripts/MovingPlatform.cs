using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // axis = x-axis (true, checked) vs y-axis(false, unchecked)
    public bool axis;
    public float movement;
    private bool leftSide=false;
    private bool rightSide=false;
    public bool onPlat = false;
    
    //public GameObject limitor;

    void Start()
    {
        leftSide = true;
        if (axis)
        { 
            movePlatformX(false);// move right
        }
        else
        { 
            movePlatformY(false);// move up
        }
    }

    // Update is called once per frame
    //direction false = left or down; direction true = right or up
    void Update()
    {
        if (rightSide==false&&axis)// moves (right)
        {
            movePlatformX(false);
        }
        else if (leftSide == false && axis)//move left
        {
            movePlatformX(true);
        }
        else if (rightSide == false && axis == false)//move up
        {
            movePlatformY(false);
        }
        else if (leftSide==false && axis==false)//move down
        {
            movePlatformY(true);
        }

    }
  //moves item direction based on editor input; movement for the platform in the x axis
    void movePlatformX(bool direction)
    {
        if (direction == false)
        {
            //below has a Time.deltaTime, but didnt seem like it was needed so left it be
            transform.position = transform.position + new Vector3(movement,0,0);
        } 
        else if (direction)
        {
            transform.position = transform.position + new Vector3( -movement, 0, 0);
        }
    }
    //movement of the platform in the y axis
    void movePlatformY(bool direction)
    {
        if (direction == false)
        {
            //below has a Time.deltaTime, but didnt seem like it was needed so left it be
            transform.position = transform.position + new Vector3(0,movement,0);
        } 
        else if (direction)
        {
            transform.position = transform.position + new Vector3(0,  -movement, 0);
        }
    }
    //collision is for the colliders to control the boundary and
    //player goes onto the moving platform
    protected void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("ColliderPlat")&&rightSide ==false)//switch to negative(left/down)
        {
            //Debug.Log("Collided with rightcollider");
            rightSide = true;
            leftSide = false;
        }
        else if (c.gameObject.CompareTag("ColliderPlat") && leftSide == false)//switch to positive(right/up)
        {
            // Debug.Log("Collided with leftcollider");
            leftSide = true;
            rightSide = false;
        }
        Debug.Log("activates onPlat statement to true");
        if (c.gameObject.CompareTag("Player"))
        {
           
            onPlat = true;
        }
    }
    //player goes off the moving platform
    protected void OnTriggerExit2D(Collider2D c)
    {
        Debug.Log("activates onPLat statement to false");
        if (c.gameObject.CompareTag("Player"))
        {
           
            onPlat = false;
        }
    }
}
