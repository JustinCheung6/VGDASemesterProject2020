using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private CameraRoom[] rooms = new CameraRoom[2];
    [SerializeField] private Orientation doorType = Orientation.NotSet;
    //[SerializeField] private LayerMask layer;
    private enum Orientation
    {
        Disabled = -1,
        NotSet = 0,
        VerticleDoor = 1,
        HorizontalDoor = 2
    }   

    public void ConnectToRooms()
    {
        if (doorType == Orientation.Disabled)
            return;
        else if(doorType == Orientation.NotSet)
        {
            SetDoor();
            if (doorType == Orientation.NotSet)
                Debug.Log("We got a problem");
        }

        LayerMask layer = 1 << LayerMask.NameToLayer("Camera Room");

        if (doorType == Orientation.HorizontalDoor)
        {
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, transform.localScale.x, layer);
            if (leftHit.collider != null)
            {

                rooms[0] = leftHit.collider.GetComponent<CameraRoom>();
                rooms[0].AddDoor(this, 0);
            }

            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, transform.localScale.x , layer);
            if (rightHit.collider != null)
            {
                rooms[1] = rightHit.collider.GetComponent<CameraRoom>();
                rooms[1].AddDoor(this, 2);
            }

            if(rooms[0] != null && rooms[1] != null)
            {
                rooms[0].AddNextRoom(rooms[1], 0);
                rooms[1].AddNextRoom(rooms[0], 2);
            }
        }
        else if(doorType == Orientation.VerticleDoor)
        {
            RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.x, layer);
            if (downHit.collider != null)
            {
                rooms[0] = downHit.collider.GetComponent<CameraRoom>();
                rooms[0].AddDoor(this, 1);
            }
            RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up, transform.localScale.x, layer);
            if (upHit.collider != null)
            {
                rooms[1] = upHit.collider.GetComponent<CameraRoom>();
                rooms[1].AddDoor(this, 3);
            }
            if (rooms[0] != null && rooms[1] != null)
            {
                rooms[0].AddNextRoom(rooms[1], 1);
                rooms[1].AddNextRoom(rooms[0], 3);
            }
        }
    }

    public void SetDoor()
    {
        if (doorType != Orientation.NotSet)
            return;

        LayerMask layer = 1 << LayerMask.NameToLayer("Camera Room");

        if (Physics2D.Raycast(transform.position, Vector2.right, transform.localScale.x, layer).collider != null)
            doorType = Orientation.HorizontalDoor;

        else if (Physics2D.Raycast(transform.position, Vector2.up, transform.localScale.y, layer).collider != null)
            doorType = Orientation.VerticleDoor;
            
        else if(Physics2D.Raycast(transform.position, Vector2.left, transform.localScale.x, layer).collider != null)
            doorType = Orientation.HorizontalDoor;

        else if (Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y, layer).collider != null)
            doorType = Orientation.VerticleDoor;
    }
}
