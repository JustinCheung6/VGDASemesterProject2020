using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [Tooltip("True if Vertical Stairs, False if Horizontal")]
    [SerializeField] private bool isVertical = true;
    [Tooltip("Direction of stair incline \n" +
        "true = +x/y direction, false = -x/y direction")]
    [SerializeField] private bool isUpPositive = false;
    void OnTriggerExit2D(Collider2D player)
    {
        Debug.Log("Stairs");
        if(isVertical)
        {
            if (isUpPositive)
            {
                if(player.transform.position.y > transform.position.y)
                    FloorManager.Get.Upstairs();
                else
                    FloorManager.Get.Downstairs();
            }
            else
            {
                if (player.transform.position.y < transform.position.y)
                    FloorManager.Get.Downstairs();
                else
                    FloorManager.Get.Upstairs();
            }
        }
        else
        {
            if (isUpPositive)
            {
                if (player.transform.position.x > transform.position.x)
                    FloorManager.Get.Upstairs();
                else
                    FloorManager.Get.Downstairs();
            }
            else
            {
                if (player.transform.position.x < transform.position.x)
                    FloorManager.Get.Downstairs();
                else
                    FloorManager.Get.Upstairs();
            }
        }
    }
}
