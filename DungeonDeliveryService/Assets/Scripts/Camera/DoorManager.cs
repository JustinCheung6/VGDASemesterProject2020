﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private GameObject doorPrefab = null;

    private List<CameraDoor> children = null;
    private Tilemap doorTilemap = null;

    private void Awake()
    {
        if (doorTilemap == null)
            doorTilemap = GetComponent<Tilemap>();
        if (children == null)
            GetChildren();
    }

    public List<CameraDoor> GetChildren()
    {
        if (children != null)
            return children;

        children = new List<CameraDoor>();

        for (int x = doorTilemap.cellBounds.xMin; x < doorTilemap.cellBounds.xMax; x++)
            for (int y = doorTilemap.cellBounds.yMin; y < doorTilemap.cellBounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);

                if (doorTilemap.HasTile(cellPos))
                {
                    Vector3 doorPosition = doorTilemap.CellToWorld(cellPos) + new Vector3(0.5f,0.5f);

                    GameObject door = Instantiate(doorPrefab, doorPosition, Quaternion.identity, transform);
                    door.GetComponent<CameraDoor>().SetDoor();
                    children.Add(door.GetComponent<CameraDoor>());
                }
            }
        return children;
    }
}