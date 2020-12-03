using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Fungus;
using UnityEngine;

public class MovingPlatform : Obstacle
{
    // axis = x-axis (true, checked) vs y-axis(false, unchecked)
    public bool axis;
    //how fast the platform moves
    public float movement;
    //if the platform has hit the left boundary
    private bool leftSide=false; 
    //if the platform has hit the right boundary
    private bool rightSide=false;
    private bool timer = false;
    //checks if the player is on the platform
    public bool onPlat = false;
    //time left until the platform is destroyed
    public float untilDestroyed;
    Animator animate;
    
    public GameObject platformAnim;
    [SerializeField] private GameObject pitGO;
    private Pit pit;


    private bool storePlat;
    private Vector2 storeLoc;
    private bool storeLeft;
    private bool storeRight;
    
    //coroutine that makes the platform wait at the end of each movement in a given time
    private IEnumerator Timer()
    {
        if (timer)
        {
            yield return new WaitForSeconds(3.0f);
            timer = false;
        }
        else if (!timer)
            yield return new WaitUntil(() =>timer);

        StartCoroutine(Timer());

    }
    //coroutine that destroys(setactive==false) the platform in a given time
    private IEnumerator Destroy()
    {
        float temp = untilDestroyed-3;
        yield return new WaitForSeconds(temp);
        animate.SetBool("Weight", true);
        yield return new WaitForSeconds(3.0f);
        TriggerObstacle();
        yield return null;
    }
    
    void Start()
    {
        animate = GetComponent<Animator>();
        pit = pitGO.GetComponent<Pit>();

        storePlat = onPlat;
        storeLeft = leftSide;
        storeRight = rightSide;
        storeLoc = (Vector2)transform.position;
        StartCoroutine(Timer());
    }
    
    void FixedUpdate()
    {
        if(!timer)
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
    //for respawning the platform with the same parameters and loc when player dies
    void respawn()
    {
        transform.position = storeLoc;
        onPlat = storePlat;
        leftSide = storeLeft;
        rightSide = storeRight;
        animate.SetBool("Weight", false);
        GetComponent<Renderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        platformAnim.GetComponent<Renderer>().enabled = true;

    }

    //moves item direction based on editor input; movement for the platform in the x axis
  //direction true = left or down; direction false = right or up
    void movePlatformX(bool direction)
    {
        if (direction == false)
        {
            transform.position =  (Vector2)transform.position + new Vector2(movement,0) * Time.fixedDeltaTime;
            
            if (onPlat)
            {
                PlayerMovement.singleton.AddTempForce(new Vector3(movement,0,0) * Time.fixedDeltaTime);
            }
        } 
        else if (direction)
        {
            transform.position =  (Vector2)transform.position + new Vector2( -movement, 0) * Time.fixedDeltaTime;
            
            if (onPlat)
            {
                PlayerMovement.singleton.AddTempForce(new Vector3( -movement, 0,0) * Time.fixedDeltaTime);
            }
      
        }
    }
    //movement of the platform in the y axis
    void movePlatformY(bool direction)
    {
        if (direction == false)
        {
            transform.position = (Vector2)transform.position + new Vector2(0,movement);
            if (onPlat)
            {
                PlayerMovement.singleton.AddTempForce(new Vector3(0,movement,0)*Time.fixedDeltaTime);
            }
        } 
        else if (direction)
        {
            transform.position =  (Vector2)transform.position + new Vector2(0,  -movement);
            if (onPlat)
            {
                PlayerMovement.singleton.AddTempForce(new Vector3(0,  -movement,0)* Time.fixedDeltaTime);
            }
        }
    }
    //collision is for the colliders to control the boundary and
    //player goes onto the moving platform
    protected override void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("ColliderPlat")&&rightSide ==false)//switch to negative(left/down)
        {
            Debug.Log("hits right");
            rightSide = true;
            timer = true;
            leftSide = false;
        }
        else if (c.gameObject.CompareTag("ColliderPlat") && leftSide == false)//switch to positive(right/up)
        {
            Debug.Log("hits left");
            leftSide = true;
            timer = true;
            rightSide = false;
        }
        if (c.gameObject.CompareTag("Player"))
        {
            if(getWeightToTrigger())
                TriggerObstacle();
            else if (player.getWeight() < weightTrigger)
            {
                StartCoroutine("Destroy");
            }
            onPlat = true;
            pit.platform = true;

        }
    }
    //player goes off the moving platform
    protected void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            onPlat = false;
            pit.platform = false;

        }
    }

    public override void TriggerObstacle()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        platformAnim.GetComponent<Renderer>().enabled = false;

    }
}
