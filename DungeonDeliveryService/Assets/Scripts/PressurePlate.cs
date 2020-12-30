using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PressurePlate : Obstacle
{
    [Tooltip("The mechanism connected to the pressure plate")]
    [SerializeField] private Mechanism mechanism;
    [Tooltip("Whether or not the mechanism is active")]
    [SerializeField] private bool needsContinuous;

    //Sprites
    [SerializeField] private Sprite plateUp;
    [SerializeField] private Sprite plateDown;

    //Narrative
    private bool quipPlayed = false;
    private string quipName = "PressurePlateQuip";

    protected void Awake()
    {
        criteria = DangerTime.greaterThanOrEqual;
        weightTrigger = 5;
    }

    protected override void MechanicReset()
    {
        DisableObstacle();
    }

    //Checks if object is a moving block or player object
    //player needs certain weight to trigger, blocks always trigger
    protected override void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.GetComponent<MovableBlock>() != null)
        {
            TriggerObstacle();
        }
        else if (c.gameObject.CompareTag("Player") && !getWeightToTrigger() && !quipPlayed)
        {
            quipPlayed = true;
            StoryManager.Get.PlayQuip(quipName, true);
        }
        else
        {
            base.OnTriggerEnter2D(c);
        }
    }

    //Disable connected mechanism if the pressure plate requires a continous force
    public void OnTriggerExit2D(Collider2D c)
    {
        if (needsContinuous)
        {
            DisableObstacle();
        }
    }

    //Activates the connected mechanism
    public override void TriggerObstacle()
    {
        if(mechanism != null)
        {
            GetComponent<SpriteRenderer>().sprite = plateDown;
            mechanism.Activate();
        }
            
    }

    //Deactivates the connected mechanism
    public void DisableObstacle()
    {
        if(mechanism != null)
        {
            GetComponent<SpriteRenderer>().sprite = plateUp;
            mechanism.Deactivate();
        }
            
    }
}
