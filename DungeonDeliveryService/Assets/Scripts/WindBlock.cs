using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WindBlock : MonoBehaviour
{
    private List<Vector2> tilesPos = null;
    [Tooltip("Used to check player's position")]
    [SerializeField] private Transform player = null;
    [SerializeField] private Wind wind = null;
    [Tooltip("Used to check direction of wind")]
    [SerializeField] private int windDirection = -1;

    [Tooltip("Moves threashold of when player is considered behind the block " +
        "(positive moves threshhold in direction of the wind)")]
    [SerializeField] private float safetyOffset = 0;
    [Tooltip("Expands threashold of when player is considered between the block " +
        "(positive expands threshhold away from block)")]
    [SerializeField] private float safetyLeniency = 0;

    [SerializeField] private TilemapCollider2D tileCollider = null;

    //Player position only used for comparing its relation from the block (if player is opposite side of blowing wind)
    private float PlayerPosBeh
    {
        get
        {
            //Wind blowing to the right
            if (windDirection == 0)
                return player.position.x;
            //Wind blowing up
            else if (windDirection == 1)
                return player.position.y;
            //Wind blowing to the left
            else if (windDirection == 2)
                return -player.position.x;
            //Wind blowing down
            else if(windDirection == 3)
                return -player.position.y;
            
            Debug.Log("PlayerComparablePos: windDirection not set.");
            return 0;
        }
    }
    //Player position only used for comparing its relation from the block (if player is between block edges)
    private float PlayerPosBetw
    {
        get
        {
            //Wind blowing to the right or left
            if (windDirection == 0 || windDirection == 2)
                return player.position.y;
            //Wind blowing up or down
            else if (windDirection == 1 || windDirection == 3)
                return player.position.x;

            Debug.Log("PlayerComparablePos: windDirection not set.");
            return 0;
        }
    }
    //Block pos only used for comparing relation from player (if player is between block edges)
    private float BlockPosBetw(int i)
    {
        //Wind blowing to the right or left
        if (windDirection == 0 || windDirection == 2)
            return tilesPos[i].y;
        //Wind blowing up or down
        else if (windDirection == 1 || windDirection == 3)
            return tilesPos[i].x;

        Debug.Log("PlayerComparablePos: windDirection not set.");
        return 0;
    }
    //Block pos only used for comparing relation from player (if player is opposite side of blowing wind)
    private float BlockPosBeh(int i)
    {
        //Wind blowing to the right
        if (windDirection == 0)
            return tilesPos[i].x;
        //Wind blowing up
        else if (windDirection == 1)
            return tilesPos[i].y;
        //Wind blowing to the left
        else if (windDirection == 2)
            return -tilesPos[i].x;
        //Wind blowing down
        else if (windDirection == 3)
            return -tilesPos[i].y;

        Debug.Log("PlayerComparablePos: windDirection not set.");
        return 0;
    }
    private void Awake()
    {
        //Get objects if not found
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        if (wind == null)
            wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<Wind>();
        if (tileCollider == null)
            tileCollider = GetComponent<TilemapCollider2D>();

        //Get tile positions. Cycle all positions in grid, and check if there's a tile there
        tilesPos = new List<Vector2>();
        Tilemap tilemap = GetComponent<Tilemap>();

        foreach (Vector3Int localPos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(localPos))
                tilesPos.Add(tilemap.CellToWorld(localPos));
        }

        //Setup variables using wind script
        windDirection = (int) (wind.GetComponent<AreaEffector2D>().forceAngle * .011f) + 1;

    }

    private void Update()
    {
        //Check if both player and windDirection was found
        if(player != null && windDirection != -1)
        {
            if (wind.StrongWinds)
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
            if (PlayerPosBetw >= (BlockPosBetw(i) - safetyLeniency) && PlayerPosBetw <= (BlockPosBetw(i) + safetyLeniency))
                if (PlayerPosBeh >= BlockPosBeh(i) + safetyOffset)
                {
                    Debug.Log("Player is Hiding");
                    return true;
                } 
        }
        Debug.Log("Player is Winded");
        return false;
    }
}
