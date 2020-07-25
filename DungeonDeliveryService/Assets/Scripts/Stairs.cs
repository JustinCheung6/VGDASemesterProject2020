using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [Tooltip("True if Vertical Stairs, False if Horizontal")]
    [SerializeField] private bool pathway;
    [Tooltip("Direction of stair incline \n" +
        "true = +x/y direction, false = -x/y direction")]
    [SerializeField] private bool upstairsDirection;
    void OnTriggerExit2D(Collider2D player)
    {
        Debug.Log("Stairs");
        if(pathway)
        {
            if (upstairsDirection)
            {
                if(player.transform.position.y > transform.position.y)
                    FloorManager.singleton.Upstairs();
                else
                    FloorManager.singleton.Downstairs();
            }
            else
            {
                if (player.transform.position.y < transform.position.y)
                    FloorManager.singleton.Downstairs();
                else
                    FloorManager.singleton.Upstairs();
            }
        }
        else
        {
            if (upstairsDirection)
            {
                if (player.transform.position.x > transform.position.x)
                    FloorManager.singleton.Upstairs();
                else
                    FloorManager.singleton.Downstairs();
            }
            else
            {
                if (player.transform.position.x < transform.position.x)
                    FloorManager.singleton.Downstairs();
                else
                    FloorManager.singleton.Upstairs();
            }
        }
    }
}
