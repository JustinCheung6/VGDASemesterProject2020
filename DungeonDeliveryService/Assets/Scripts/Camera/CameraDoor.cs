using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDoor : MonoBehaviour
{
    [SerializeField] private CameraRoom[] rooms = new CameraRoom[2];
    [SerializeField] private Orientation doorType = Orientation.NotSet;

    private Vector2[] entrancePos = new Vector2[2];

    //Object References
    private Collider2D collider = null;
    private Transform cameraDummy = null;
    private PlayerMovement pmScript = null;
    private CameraManager cmScript = null;

    private enum Orientation
    {
        Disabled = -1,
        NotSet = 0,
        VerticleDoor = 1,
        HorizontalDoor = 2
    }

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        cameraDummy = GameObject.FindGameObjectWithTag("Camera Dummy").transform;
        pmScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        cmScript = FindObjectOfType<CameraManager>();
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

        entrancePos[0] = entrancePos[1] = transform.position;
        if (doorType == Orientation.HorizontalDoor)
        {
            entrancePos[0].x -= 2;
            entrancePos[1].x += 2;
        }
        else if (doorType == Orientation.VerticleDoor)
        {
            entrancePos[0].y -= 2;
            entrancePos[1].y += 2;
        }
    }

    private IEnumerator TravelThroughDoor(Vector2 destination, CameraRoom room)
    {
        
        pmScript.AddRestrictions();
        yield return new WaitForSeconds(0.1f);
        

        yield return StartCoroutine(cmScript.SetupDoorAnim(destination, room));

        collider.enabled = false;
        yield return StartCoroutine(pmScript.WalkToDoor(destination));
        collider.enabled = true;
        pmScript.RemoveRestrictions();
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            Vector2 destination = transform.position;
            Vector2Int distance = Vector2Int.zero;
            CameraRoom cr = null;

            if (doorType == Orientation.VerticleDoor)
            {
                distance.y = (c.gameObject.transform.position.y > transform.position.y) ? -1 : 1;
                destination.y += 2 * distance.y;
                cr = (distance.y > 0) ? rooms[1] : rooms[0]; 
            }
                
            else if(doorType == Orientation.HorizontalDoor)
            {
                distance.x = (c.gameObject.transform.position.x > transform.position.x) ? -1 : 1;
                destination.x += 2 * distance.x;
                cr = (distance.x > 0) ? rooms[1] : rooms[0];
            }
                

            StartCoroutine(TravelThroughDoor(destination, cr));
        }
    }
}
