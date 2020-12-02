using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SealedDoor : Mechanism
{
    [SerializeField] protected TilemapCollider2D TC;
    [SerializeField] private TilemapRenderer TR;

    //Activate method for the sealed door
    public override void Activate()
    {
        TR.enabled = false;
        TC.enabled = false;
    }

    //Deactivate method for the sealed door
    public override void Deactivate()
    {
        TR.enabled = true;
        TC.enabled = true;
    }
}
