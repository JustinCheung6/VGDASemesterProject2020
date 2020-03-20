using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Grid grid = null;
    private Transform playerTrans = null;

    private Vector3 startingCellPos = Vector3.zero;
    //Gets and stores the values of the grid cell sizes ([0]: full size, [1]: half size, [2]: quarter size)
    private Vector3[] cellSize = new Vector3[3];
    //Multiple used to covert World position to cell position
    private Vector3 cellConversion = Vector3.zero;

    [SerializeField] private Vector3[] playerCellPos = { Vector3.zero, Vector3.zero };

    void Awake()
    {
        //Initialize all the object variables
        if(grid == null)
        {
            if (GetComponentInParent<Grid>() != null)
                grid = GetComponentInParent<Grid>();
            else if (GetComponent<Grid>() != null)
                grid = GetComponent<Grid>();
        }

        if (playerTrans == null)
            playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //Initialize the rest of the variables
        cellSize[0] = grid.cellSize;
        cellSize[1] = cellSize[0] / 2;
        cellSize[2] = cellSize[1] / 2;

        cellConversion.x = 1 / cellSize[0].x;
        cellConversion.y = 1 / cellSize[0].y;

        startingCellPos = grid.transform.position + cellSize[1];
    }

    // Update is called once per frame
    void Update()
    {
        playerCellPos = WorldtoCell(playerTrans.position);
    }

    public Vector3[] WorldtoCell(Vector3 position)
    {
        Vector2 tempCellPos = Vector2.zero;
        Vector3[] finalCellPos = { Vector3.zero, Vector3.zero };

        tempCellPos.x = ((position.x - startingCellPos.x) * cellConversion.x);
        if(tempCellPos.x > 0)
        {
            if (tempCellPos.x - (int)tempCellPos.x >= (cellSize[1].x + cellSize[2].x))
            {
                finalCellPos[1].x = finalCellPos[0].x = (int)tempCellPos.x + 1;
            }
            else if (tempCellPos.x - (int)tempCellPos.x >= cellSize[1].x)
            {
                finalCellPos[0].x = (int)tempCellPos.x + 1;
                finalCellPos[1].x = (int)tempCellPos.x + 0.5f;
            }
            else if (tempCellPos.x - (int)tempCellPos.x >= cellSize[2].x)
            {
                finalCellPos[0].x = (int)tempCellPos.x;
                finalCellPos[1].x = (int)tempCellPos.x + 0.5f;
            }
            else
            {
                finalCellPos[1].x = finalCellPos[0].x = (int)tempCellPos.x;
            }

        }
        else if(tempCellPos.x != 0)
        {
            if (tempCellPos.x - (int)tempCellPos.x >= -cellSize[2].x)
            {
                finalCellPos[1].x = finalCellPos[0].x = (int)tempCellPos.x + 1;
            }
            else if (tempCellPos.x - (int)tempCellPos.x >= -cellSize[1].x)
            {
                finalCellPos[0].x = (int)tempCellPos.x + 1;
                finalCellPos[1].x = (int)tempCellPos.x + 0.5f;
            }
            else if (tempCellPos.x - (int)tempCellPos.x >= -(cellSize[2].x + cellSize[1].x))
            {
                finalCellPos[0].x = (int)tempCellPos.x;
                finalCellPos[1].x = (int)tempCellPos.x + 0.5f;
            }
            else
            {
                finalCellPos[1].x = finalCellPos[0].x = (int)tempCellPos.x;
            }
        }

        tempCellPos.y = ((position.y - startingCellPos.y) * cellConversion.y);
        if (tempCellPos.y - (int)tempCellPos.y >= -cellSize[2].y)
        {
            finalCellPos[1].y = finalCellPos[0].y = (int)tempCellPos.y + 1;
        }
        else if (tempCellPos.y - (int)tempCellPos.y >= -cellSize[1].y)
        {
            finalCellPos[0].y = (int)tempCellPos.y + 1;
            finalCellPos[1].y = (int)tempCellPos.y + 0.5f;
        }
        else if (tempCellPos.y - (int)tempCellPos.y >= -(cellSize[2].y + cellSize[1].y))
        {
            finalCellPos[0].y = (int)tempCellPos.y;
            finalCellPos[1].y = (int)tempCellPos.y + 0.5f;
        }
        else
        {
            finalCellPos[1].y = finalCellPos[0].y = (int)tempCellPos.y;
        }
        return finalCellPos;
    }
}
