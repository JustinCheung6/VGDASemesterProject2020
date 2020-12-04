using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement singleton;

    // Set user moveSpeed for DeliveryPerson.
    public float moveSpeed = 5.0f;
    // Get the Object for the DeliveryPerson
    public Rigidbody2D rb;
    public float acceleration = 1.0f;
    // Set up the movement variables that will be called.
    private float moveHorizontal, moveVertical;
    [SerializeField] private bool playingAnimation = false;
    public bool PlayAnimation { set => playingAnimation = value; }
    //Check if player is moving by the controller
    private bool controllerMovement = false;

    //Forces from other scripts (reset on FixedUpdate)
    private Vector2 tempExternal = new Vector2();
    private Dictionary<string, Vector2> externalForces = new Dictionary<string, Vector2>();
    private Vector2 constantExternal = new Vector2();

    public bool IsMoving { get => (!playingAnimation && controllerMovement); }

    // Checks if direction is disabled because of wind: 0 = Left, 1 = Down, 2 = Right, 3 = Up
    [SerializeField] private int[] restrinctions = { 0, 0, 0, 0 };

    private Vector2 lastFramePos = new Vector2();
    [SerializeField] private bool hasMoved = false;
    public bool HasMoved { get => hasMoved; }

    //Stops player movement at a specfic direction
    public void AddRestrictions(int index)
    {
        restrinctions[index] += 1;
    }
    //Stops player movement altogether
    public void AddRestrictions()
    {
        rb.velocity = Vector3.zero;

        for (int i = 0; i < restrinctions.Length; i++)
        {
            restrinctions[i] += 1;
        }
    }
    //Removes a restrction from player movement at a specific direction
    public void RemoveRestrictions(int index)
    {
        restrinctions[index] -= 1;
    }
    //Removes a restrction from player movement in all directions
    public void RemoveRestrictions()
    {
        for (int i = 0; i < restrinctions.Length; i++)
        {
            restrinctions[i] -= 1;
        }
    }

    //Adds tempoarary Movement to Player from external scripts
    public void AddTempForce(Vector2 positionUpdate)
    {
        tempExternal += positionUpdate;
    }
    //Add a force that will constantly move the player (Transform.position, Fixed Update)
    public void AddConstForce(string id, Vector2 positionUpdate)
    {
        if (externalForces.ContainsKey(id))
            Debug.Log("ID: " + id + " already exists in PlayerMovement");
        else
        {
            externalForces.Add(id, positionUpdate);
            constantExternal += positionUpdate;
        }
    }
    //Remove a force that was added by giving its previously used id
    public void RemoveConstForce(string id)
    {
        if (!externalForces.ContainsKey(id))
            Debug.Log("ID: " + id + " can't be found in PlayerMovement");
        else
        {
            constantExternal -= externalForces[id];
            externalForces.Remove(id);
            
        }
        
    }

    public bool HasConstForce(string id)
    {
        return (externalForces.ContainsKey(id));
    }

    private void Awake()
    {
        //Setup Singleton
        if(singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Debug.Log("Multiple PlayerMovements Scripts found Saved: " + singleton.name + ", Unsaved Copy: " + this.name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lastFramePos = transform.position;
    } 

    // Update is called once per frame
    void Update()
    {
        if (!playingAnimation)
        {
            // Get the respective Horizontal and Vertical movement.
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }

    } // Close Update

    private void FixedUpdate()
    {
        if (!playingAnimation)
        {
            // Calculate the player's net movement.
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            //Check if player is moving at all
            if (movement == Vector2.zero)
            {
                controllerMovement = false;
            }
            else
            {
                controllerMovement = true;

                //Set movement to zero if movement is restricted in a specific direction
                if (movement.x < 0 && restrinctions[0] > 0)
                    movement.x = 0;
                else if (movement.x > 0 && restrinctions[2] > 0)
                    movement.x = 0;
                else
                    //This prevents player from being affected by area effector after it's been turned off
                    rb.velocity = new Vector2(0, rb.velocity.y);

                if (movement.y < 0 && restrinctions[1] > 0)
                    movement.y = 0;
                else if (movement.y > 0 && restrinctions[3] > 0)
                    movement.y = 0;
                else
                    //This prevents player from being affected by area effector after it's been turned off
                    rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            // Apply the actual movement
            transform.position = ((Vector2)transform.position) + (movement * moveSpeed * Time.fixedDeltaTime * 10f) + tempExternal + constantExternal;
            tempExternal = new Vector2();


            hasMoved = (lastFramePos.x != transform.position.x || lastFramePos.y != transform.position.y) ? true : false;
            lastFramePos = new Vector2(transform.position.x, transform.position.y);
        }
        else
            controllerMovement = false;
    } 
    public void WalkToDoor(Vector2 destination)
    {
        transform.position = destination;
    }
} //Close PlayerMovement
