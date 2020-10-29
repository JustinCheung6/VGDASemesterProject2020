using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraManager : MonoBehaviour
{
    //Room Prefab with Bounding box collider for Cinemachine
    [SerializeField] private GameObject roomPrefab = null;

    private CameraRoom[,] roomLayout = null;
    private Vector2Int originRoom = new Vector2Int();
    private Vector2Int playerRoom = new Vector2Int();

    //Object References
    [Tooltip("Finds object with Room Outline tag for setting up rooms")][SerializeField] private Tilemap roomOutline = null;
    private Camera mainCam = null;
    private Cinemachine.CinemachineVirtualCamera vcam = null;
    private Cinemachine.CinemachineConfiner boundingBox = null;
    private Transform dummyCamera = null;
    private Transform playerTrans = null;

    private void Awake()
    {
        roomOutline = GameObject.FindGameObjectWithTag("Room Outline").GetComponent<Tilemap>();
        vcam = GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
        boundingBox = GetComponentInChildren<Cinemachine.CinemachineConfiner>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        dummyCamera = GameObject.FindGameObjectWithTag("Camera Dummy").transform;
        mainCam = GetComponentInChildren<Camera>();

        List<CameraRoom> rooms = new List<CameraRoom>();
        bool originFound = false;

        for(int x = roomOutline.cellBounds.xMin; x < roomOutline.cellBounds.xMax; x++)
            for(int y = roomOutline.cellBounds.yMin; y < roomOutline.cellBounds.yMax; y++)
            {
                if (roomOutline.HasTile(new Vector3Int(x,y,0)))
                {
                    if (!originFound)
                    {
                        if (roomOutline.HasTile(new Vector3Int(x, y - 1, 0)) && roomOutline.HasTile(new Vector3Int(x - 1, y, 0)))
                        {
                            rooms.Insert(0, AddRoom(new int[2] { x, y }, true));
                        } 
                        else if (roomOutline.HasTile(new Vector3Int(x, y + 1, 0)) && roomOutline.HasTile(new Vector3Int(x + 1, y, 0)))
                            rooms.Add(AddRoom(new int[2] { x, y }));
                    }
                    else if (roomOutline.HasTile(new Vector3Int(x, y + 1, 0)) && roomOutline.HasTile(new Vector3Int(x + 1, y, 0)))
                        rooms.Add(AddRoom(new int[2] { x, y }));
                }
            }

        List<CameraDoor> doors = GameObject.FindGameObjectWithTag("Door Manager").GetComponent<DoorManager>().GetChildren();
        foreach (CameraDoor door in doors)
            door.ConnectToRooms();

        int[] dungeonSize = rooms[0].IndexRooms(0, 0);
        //Debug.Log("Dungeon Size. X: " + dungeonSize[0] + " Y: " + dungeonSize[1] + " -X: " + dungeonSize[2] + " -Y: " + dungeonSize[3]);

        originRoom = -(new Vector2Int(dungeonSize[2], dungeonSize[3]));
        roomLayout = new CameraRoom[dungeonSize[0] - dungeonSize[2] + 1, dungeonSize[1] - dungeonSize[3] + 1];
        //Debug.Log(dungeonSize[0] - dungeonSize[2] + ", " + (dungeonSize[1] - dungeonSize[3]));
        foreach (CameraRoom room in rooms)
        {
            //Debug.Log(room.x + ", " + room.y);
            //.Log((room.x + originRoom.x) + ", " + (room.y + originRoom.y));
            roomLayout[room.x + originRoom.x, room.y + originRoom.y] = room;
        }

        playerRoom = originRoom;

        //Setup Camera
        vcam.Follow = playerTrans;
        boundingBox.m_BoundingShape2D = roomLayout[playerRoom.x, playerRoom.y].GetComponent<Collider2D>();
    }
    private CameraRoom AddRoom(int[] pos, bool origin = false)
    {
        Vector3Int?[] bounds = { null, null };

        for (int i = 0; i < 2; i++)
        {
            int offset = 1;
            bool skip = false;
            int plsDontBreakGameCounter = 0;
            int[] checkPos = { pos[0], pos[1] };
            while (!bounds[i].HasValue && plsDontBreakGameCounter < 100)
            {   
                checkPos[i] += (origin) ? -offset : offset;
                if (roomOutline.HasTile(new Vector3Int(checkPos[0], checkPos[1], 0)))
                {
                    if (!skip)
                        skip = true;
                    else
                        bounds[i] = new Vector3Int(checkPos[0], checkPos[1], 0);
                }
                plsDontBreakGameCounter++;
            }
        }

        Vector3Int bottomLeft = origin ? new Vector3Int(bounds[0].Value.x, bounds[1].Value.y, 0) : new Vector3Int(pos[0], pos[1], 0);
        Vector3Int topRight = origin ? new Vector3Int(pos[0], pos[1], 0) : new Vector3Int(bounds[0].Value.x, bounds[1].Value.y, 0);

        //Debug.Log("Bottom Left: " + bottomLeft + " Top Right: " + topRight);

        Vector3 size = roomOutline.CellToWorld(topRight) - roomOutline.CellToWorld(bottomLeft) + (Vector3)Vector2.one;
        Vector3 position = roomOutline.CellToWorld(bottomLeft) + (size / 2);

        GameObject room = Instantiate(roomPrefab, position, Quaternion.identity, transform);
        room.GetComponentInChildren<BoxCollider2D>().size = size;

        return room.GetComponent<CameraRoom>();
    }

    public IEnumerator SetupDoorAnim(Vector2 destination, CameraRoom newRoom)
    {
        playerRoom = newRoom.position;

        //boundingBox.m_BoundingShape2D = null;
        dummyCamera.position = destination;
        dummyCamera.gameObject.SetActive(true);
        dummyCamera.GetComponent<Cinemachine.CinemachineConfiner>().m_BoundingShape2D = newRoom.GetComponent<Collider2D>();
        vcam.Follow = null;
        boundingBox.m_BoundingShape2D = newRoom.GetComponent<Collider2D>();

        /*
        while ((Vector2)mainCam.transform.position != destination)
        {
           //Vector3 travel = Vector2.MoveTowards(mainCam.transform.position, destination, 0.3f);
            Vector3 travel = destination;
            travel.z = -10;

            Debug.Log("Cam Moving");
            mainCam.transform.position = travel;
            yield return new WaitForFixedUpdate();
        }
        */
        //Vector3 travel = Vector2.MoveTowards(mainCam.transform.position, destination, 0.3f);
        Vector3 travel = destination;
        travel.z = -10;

        Debug.Log("Cam Moving");
        mainCam.transform.position = travel;
        yield return new WaitForFixedUpdate();

        dummyCamera.GetComponent<Cinemachine.CinemachineConfiner>().m_BoundingShape2D = null;
        boundingBox.m_BoundingShape2D = newRoom.GetComponent<Collider2D>();
        vcam.Follow = playerTrans;
        dummyCamera.gameObject.SetActive(false);
    }
}
