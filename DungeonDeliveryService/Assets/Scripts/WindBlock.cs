using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WindBlock : MonoBehaviour
{
    private List<Vector2> tilesPos = null;
    [Tooltip("Used to check direction of wind")]
    [SerializeField] private int windDirection = -1;

    [Header("Object References")]
    [Tooltip("Used to check player's position")]
    [SerializeField] private Transform player = null;
    [SerializeField] private Wind wind = null;
    [SerializeField] private TilemapCollider2D tileCollider = null;
    [SerializeField] private GridManager gridManager = null;
    private void Awake()
    {
        //Get objects if not found
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        if (wind == null)
            wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<Wind>();
        if (tileCollider == null)
            tileCollider = GetComponent<TilemapCollider2D>();
        if (gridManager == null)
            gridManager = GetComponentInParent<GridManager>();

        //Get tile positions. Cycle all positions in grid, and check if there's a tile there
        tilesPos = new List<Vector2>();
    }

    private void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        foreach (Vector3Int localPos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(localPos))
            {
                Vector3 tempBlockPos = tilemap.CellToWorld(localPos);
                tempBlockPos.x += 0.5f;
                tempBlockPos.y += 0.5f;
                Vector3[] finalBlockPos = gridManager.WorldtoCell(tempBlockPos);

                tilesPos.Add(finalBlockPos[0]);
                Debug.Log(tempBlockPos);
                Debug.Log(finalBlockPos[0]);
            }
        }

        //Setup variables using wind script
        windDirection = (int)(wind.GetComponent<AreaEffector2D>().forceAngle * .011f) + 1;
    }

    private void Update()
    {
        //Check if both player and windDirection was found
        if(player != null && windDirection != -1)
        {
            if (wind.WindReady)
            {
                wind.ToggleBlocked(CheckBlocked());
            }
            else
                wind.ToggleBlocked(false);
        }
           
    }

    private bool CheckBlocked()
    {
        //Check if player is behind the block (not sticking out in contact with wind)
        for(int i = 0; i < tilesPos.Count; i++)
        {

            //Checks if the player is at the correct side of the block (opposite of wind)
            if (tilesPos[i].y >= gridManager.PlayerCellPos[0].y)
                if (tilesPos[i].x == gridManager.PlayerCellPos[0].x)
                {
                    Debug.Log("Player is Hiding");
                    return true;
                } 
        }
        Debug.Log("Player is Winded");
        return false;
    }
}
