using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // axis = x-axis vs y-axis
    public bool axis;
    public int limLeft;
    public int movement;
    public bool leftSide=false;
    public bool rightSide=false;

    // Update is called once per frame
    //direction false = left or down; direction true = right or up
    void Update()
    {
        if (leftSide == true)
        {
            movePlatformX(false);
            leftSide = false;
        }
        else if (rightSide == true)
        {
            movePlatformX(true);
            rightSide = false;
        }

        //movePlatform();
    }
  //moves item direction based on editor input
    void movePlatformX(bool direction)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (direction == false)
        {
            //below has a Time.deltaTime, but didnt seem like it was needed so left it be
            transform.position = transform.position + new Vector3(horizontalInput *movement,verticalInput,0);
        } 
        else if (direction == true)
        {
            transform.position = transform.position + new Vector3(horizontalInput * -movement, verticalInput, 0);
        }
    }

    void movePlatformY(bool direction)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (direction == false)
        {
            //below has a Time.deltaTime, but didnt seem like it was needed so left it be
            transform.position = transform.position + new Vector3(horizontalInput,verticalInput *movement,0);
        } 
        else if (direction == true)
        {
            transform.position = transform.position + new Vector3(horizontalInput, verticalInput * -movement, 0);
        }
    }
    
}
