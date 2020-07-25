using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public static FloorManager singleton = null;

    [SerializeField] private Collider2D firstFloorCollider = null;
    [SerializeField] private Collider2D secondFloorCollider = null;


    [Tooltip("True if starting upstairs, False if starting downstairs")]
    [SerializeField] private bool upStairs;

    private void Awake()
    {
        if(FloorManager.singleton == null)
        {
            FloorManager.singleton = this;
        }
        else if(FloorManager.singleton != this)
        {
            Debug.Log("Extra singleton found for FloorManager. " + this);
            Destroy(gameObject);
        }

        if (upStairs)
            Upstairs();
        else
            Downstairs();
    }

    public void Upstairs()
    {
        if (firstFloorCollider != null && secondFloorCollider != null)
        {
            Debug.Log("Going Up");
            firstFloorCollider.enabled = false;
            secondFloorCollider.enabled = true;
            upStairs = true;
        }
        else
        {
            Debug.Log("Floor Colliders not set in FloorManager");
        }
            
    }

    public void Downstairs()
    {
        if (firstFloorCollider != null && secondFloorCollider != null)
        {
            Debug.Log("Going Down");
            firstFloorCollider.enabled = true;
            secondFloorCollider.enabled = false;
            upStairs = false;
        }
        else
        {
            Debug.Log("Floor Colliders not set in FloorManager");
        }
    }
}
