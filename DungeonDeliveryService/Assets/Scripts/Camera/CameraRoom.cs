using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoom : MonoBehaviour
{
    //Door object that are connected to this room (0 = right wall, 1 = top, 2 = left, 3 = bottom)
    [SerializeField] private CameraDoor[] doors = new CameraDoor[4];
    [SerializeField] private CameraRoom[] rooms = new CameraRoom[4];
    //Position of room relative to the origin room
    private int[] gridPosition = null;

    public int x { get => gridPosition[0]; }
    public int y { get => gridPosition[1]; }
    public Vector2Int position { get => new Vector2Int(gridPosition[0], gridPosition[1]); }

    public void AddDoor(CameraDoor door, int index)
    {
        if (doors[index] != null)
            return;
        doors[index] = door;
    }
    public void AddNextRoom(CameraRoom room, int index)
    {
        if (rooms[index] != null)
            return;
        rooms[index] = room;
    }

    public int[] IndexRooms(int x, int y, int negX = 0, int negY = 0)
    {
        if (gridPosition != null)
            return new int[] { x, y, negX, negY };
        gridPosition = new int[] { x, y };
        
        int[] thisRoomData = new int[] { x, y, negX, negY };

        for(int i = 0; i < rooms.Length; i++)
        {
            //Debug.Log(rooms[i]);
            if (rooms[i] != null)
            {
                int[] tempData = thisRoomData;
                if (i <= 1)
                    tempData[i]++;
                else
                    tempData[i]--;

                int[] nextRoomData = rooms[i].IndexRooms(tempData[0], tempData[1], tempData[2], tempData[3]);
                if (thisRoomData[0] < nextRoomData[0])
                    thisRoomData[0] = nextRoomData[0];
                if (thisRoomData[1] < nextRoomData[1])
                    thisRoomData[1] = nextRoomData[1];
                if (thisRoomData[2] > nextRoomData[2])
                    thisRoomData[2] = nextRoomData[2];
                if (thisRoomData[3] > nextRoomData[3])
                    thisRoomData[3] = nextRoomData[3];
            }
        }

        return thisRoomData;
    }

    
}
