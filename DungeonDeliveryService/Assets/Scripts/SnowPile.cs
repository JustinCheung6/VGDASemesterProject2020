using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SnowPile : Obstacle
{
    [SerializeField] private Tilemap snowPiles = null;
    [SerializeField] private TilemapCollider2D tileCollider2D = null;

    private Dictionary<Vector3, bool> tilePos = new Dictionary<Vector3, bool>();
    //Information for duplicating snowpiles
    private List<Vector3Int> ogPosition = new List<Vector3Int>();
    //Images for the snow object (middle, top, left, right, bottom)
    private Sprite[] snow = { null, null, null, null, null };

    protected override void Awake()
    {
        base.Awake();
        snowPiles = GetComponent<Tilemap>();
        tileCollider2D = GetComponent<TilemapCollider2D>();
    }

    private void Start()
    {
        SetStairs(GetStairs());
        TrackSnowPiles();
    }

    private void TrackSnowPiles()
    {
        snowPiles.CompressBounds();

        foreach (Vector3Int pos in snowPiles.cellBounds.allPositionsWithin)
        {
            bool hasTile = false;
            Vector3 cellPos = cellPos = GridManager.Get.WorldtoCell(new Vector3(pos.x + 0.51f, pos.y + 0.51f, pos.z))[1];
            //Debug.Log("Position1: " + cellPos);

            if (snowPiles.HasTile(pos))
            {
                ogPosition.Add(pos);
                hasTile = true;
            }
            if(!tilePos.ContainsKey(cellPos))
                tilePos.Add(cellPos, hasTile);
        }
    }

    //Sets collider isTrigger based on whether player is upstairs or not
    public void SetStairs(bool upStairs)
    {
        tileCollider2D.isTrigger = upStairs;
    }
    //Getter for FloorManager 
    public bool GetStairs()
    {
        if (FloorManager.Get == null)
        {
            Debug.Log("Warning: FloormManager is empty");
            return false;
        }
        return FloorManager.Get.UpStairs;
    }

    //Check if player is on top of a snowblock
    protected override void UpdateObstacle()
    {
        //Checks if player is on top of snow pile 
        if (tilePos.ContainsKey(GridManager.Get.PlayerCellPos[1]))
            if (tilePos[GridManager.Get.PlayerCellPos[1]])
                TriggerObstacle();
    }
    //Delete tile and drop player down to bottom floor
    public override void TriggerObstacle()
    {
        Vector3Int deletePos = snowPiles.WorldToCell(FindObjectOfType<PlayerMovement>().transform.position);

        snowPiles.SetTile(deletePos, null);

        tilePos[GridManager.Get.PlayerCellPos[0]] = false;
        FloorManager.Get.Downstairs();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if(getWeightToTrigger())
            GridManager.Get.OnMoved += UpdateObstacle;
    }
    protected void OnTriggerExit2D(Collider2D col)
    {
        if(getWeightToTrigger())
            GridManager.Get.OnMoved -= UpdateObstacle;
    }
    //Remove default obstacle OnCollisionEnter function
    public override void OnCollisionEnter2D(Collision2D col)
    {

    }
}
