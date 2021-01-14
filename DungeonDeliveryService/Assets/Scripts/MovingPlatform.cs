using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovingPlatform : Obstacle
{

    
    [Header("MovingPlatform")]
    // axis = x-axis (true, checked) vs y-axis(false, unchecked)
    public bool axis; 
    //how fast the platform moves
    [SerializeField] private float movement = 1f;

    //if the platform has hit the left boundary
    private bool leftSide=false; 
    //if the platform has hit the right boundary
    private bool rightSide=false;

    //time left until the platform is destroyed
    public float untilDestroyed;
    private bool platWaiting = false;
    //checks if the player is on the platform
    [HideInInspector] private bool onPlat = false;

    Animator animate;

    //Is true of the platform has been spawned
    private bool spawned = true;

    public GameObject platformAnim;
    [SerializeField] private GameObject pitGO;
    private Pit pit;

    [Header("Time")]
    [Tooltip("Time it takes to get from 1 side of the platform to the other (Max ~50)")]
    [SerializeField] private int moveTime = 1;
    private int destinationTime = 0;

    //[Time Manager]
    private static MovingPlatform timeManager = null;
    //static timer that counts from 0 - 100 and then resets
    private static int clock = 0;
    //timer that counts to 1 second
    private static float timer = 0f;

    [Header("Debugging")]
    [Tooltip("Check true to debug platform speed")]
    [SerializeField] private bool testing = false;
    //Accuracy of speed to get to desiredTime. # of decimals allowed (Max 5)
    private int accuracy = 0;

    

    //[Respawn Info]
    private bool storePlat;
    private Vector2 storeLoc;
    private bool storeLeft;
    private bool storeRight;

    //position of colliders 
    private float[] platColliders = new float[2];
    
    //coroutine that destroys(setactive==false) the platform in a given time
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(()=> !platWaiting);
        float temp = untilDestroyed-2f;
        if(temp > 0)
        {
            yield return new WaitForSeconds(temp);
            animate.SetBool("Weight", true);
            yield return new WaitForSeconds(2.0f);
        }
        else
        {
            animate.SetBool("Weight", true);
            yield return new WaitForSeconds(untilDestroyed);
        }
        TriggerObstacle();
        animate.SetBool("Weight", false);
        yield return new WaitForSeconds(3.0f);
        if (!spawned)
            respawn();
        yield return null;
    }

    private void OnEnable()
    {
        if (timeManager == null)
            timeManager = this;
    }
    private void OnDisable()
    {
        if(timeManager == this)
        {
            MovingPlatform plat = FindObjectOfType<MovingPlatform>();
            timeManager = (plat != null) ? plat : null;
        }
    }

    void Start()
    {
        if (testing)
            movement = 1f;

        animate = GetComponent<Animator>();
        pit = pitGO.GetComponent<Pit>();

        //Set endpoints of movingplatforms
        BoxCollider2D[] cols = GetComponentsInChildren<BoxCollider2D>();

        rightSide = (transform.position.x > cols[1].transform.position.x ||
            transform.position.y > cols[1].transform.position.y);
        leftSide = !rightSide;

        if (axis)
        {
            platColliders[1] = (transform.position.x > cols[1].transform.position.x) ? 
                transform.position.x : cols[1].transform.position.x;
            platColliders[0] = (transform.position.x > cols[1].transform.position.x) ?
                cols[1].transform.position.x : transform.position.x;
        }
        else
        {
            platColliders[1] = (transform.position.y > cols[1].transform.position.y) ?
                transform.position.y : cols[1].transform.position.y;
            platColliders[0] = (transform.position.y > cols[1].transform.position.y) ?
                cols[1].transform.position.y : transform.position.y;
        }

        destinationTime = clock + moveTime;
        Destroy(cols[1].gameObject);

        storePlat = onPlat;
        storeLeft = leftSide;
        storeRight = rightSide;
        storeLoc = (Vector2)transform.position;
    }
    
    void FixedUpdate()
    {
        if (clock % 10 == 0)
            Debug.Log("Tick: " + clock);
        //Delta Time
        if(this == timeManager)
        {
            if(timer >= 1f)
            {
                timer = 0;
                clock = (clock < 100) ? clock + 1 : 0;
            }
            timer += Time.fixedDeltaTime;
        }

        //Platform waits when at edge
        if(platWaiting && JobsDone())
        {
            platWaiting = false;
            destinationTime = clock + moveTime;
        }
        //Move Platform
        if(!platWaiting)
            if (rightSide==false && axis)// moves (right)
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
        spawned = true;
        transform.position = storeLoc;
        onPlat = storePlat;
        leftSide = storeLeft;
        rightSide = storeRight;
        animate.SetBool("Weight", false);
        GetComponent<BoxCollider2D>().enabled = true;
        platformAnim.GetComponent<SpriteRenderer>().enabled = true;
        platWaiting = true;

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
    
        //Going left
        if(direction && (transform.position.x <= platColliders[0] || JobsDone()) )
        {
            if (testing)
                UpdateSpeed_Debug(transform.position.x > platColliders[0]);

            transform.position = new Vector3(platColliders[0], transform.position.y);
            SwitchDirections();
        }
        else if (!direction && (transform.position.x >= platColliders[1] || JobsDone()) )
        {
            if (testing)
                UpdateSpeed_Debug(transform.position.x < platColliders[1]);

            transform.position = new Vector3(platColliders[1], transform.position.y);
            SwitchDirections();
        }

    }
    //movement of the platform in the y axis
    void movePlatformY(bool direction)
    {
        if (direction == false)
        {
            transform.position = (Vector2)transform.position + new Vector2(0,movement) * Time.fixedDeltaTime;
            if (onPlat)
            {
                PlayerMovement.singleton.AddTempForce(new Vector3(0,movement,0)*Time.fixedDeltaTime);
            }
        } 
        else if (direction)
        {
            transform.position =  (Vector2)transform.position + new Vector2(0,  -movement) * Time.fixedDeltaTime;
            if (onPlat)
            {
                PlayerMovement.singleton.AddTempForce(new Vector3(0,  -movement,0)* Time.fixedDeltaTime);
            }
        }

        //Going left
        if (direction && (transform.position.y <= platColliders[0] || JobsDone()) )
        {
            if (testing)
                UpdateSpeed_Debug(transform.position.y > platColliders[0]);

            transform.position = new Vector3(transform.position.x, platColliders[0]);
            SwitchDirections();
        }
        else if (!direction && (transform.position.y >= platColliders[1] || JobsDone()) )
        {
            if (testing)
                UpdateSpeed_Debug(transform.position.y < platColliders[1]);

            transform.position = new Vector3(transform.position.x, platColliders[1]);
            SwitchDirections();
        }
    }
    //collision is for the colliders to control the boundary and
    //player goes onto the moving platform
    
    private void SwitchDirections()
    {
        destinationTime = clock + 1;
        platWaiting = true;

        if (rightSide == false) //switch to negative(left/down)
        {
            //Debug.Log("hits right");
            rightSide = true;
            
            leftSide = false;
        }
        else if (leftSide == false) //switch to positive(right/up)
        {
            //Debug.Log("hits left");
            leftSide = true;
            rightSide = false;
        }
    }
    
    private bool JobsDone()
    {
        if (destinationTime > 100)
        {
            if (clock >= 50)
                return false;
            return (destinationTime - 101) <= clock;
        }
        return destinationTime <= clock;
    }

    protected override void OnTriggerEnter2D(Collider2D c)
    {   
        if (c.gameObject.CompareTag("Player"))
        {
            Debug.Log("hits Player");
            if (getWeightToTrigger())
                TriggerObstacle();
            else if (Player.Get.getWeight() < weightTrigger)
            {
                StartCoroutine(Destroy());
            }
            onPlat = true;
            pit.Platform = true;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D c)
    {

    }

    //player goes off the moving platform
    protected void OnTriggerExit2D (Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            onPlat = false;
            pit.Platform = false;
        }
    }

    public override void TriggerObstacle()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        platformAnim.GetComponent<SpriteRenderer>().enabled = false;
        spawned = false;
    }

    private void UpdateSpeed_Debug(bool isBehind)
    {
        float temp = 0;
        switch (accuracy)
        {

            case 0:
                temp = 1f;
                break;
            case 1:
                temp = 0.1f;
                break;
            case 2:
                temp = 0.01f;
                break;
            case 3:
                temp = 0.001f;
                break;
            case 4:
                temp = 0.0001f;
                break;
            case 5:
                temp = 0.00001f;
                break;
        }
        if (isBehind)
            movement += temp;
        else if(accuracy == 5)
        {
            Debug.Log("Final Speed for " + this.gameObject.name + " is: " + movement);
            testing = false;
        }
        else
        {
            movement -= temp;
            accuracy++;
        }
    }
}
