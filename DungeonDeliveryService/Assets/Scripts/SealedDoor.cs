using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SealedDoor : Mechanism
{
    [SerializeField] protected TilemapCollider2D TC;
    [SerializeField] private TilemapRenderer TR;
    public override void Activate()
    {
        TR.enabled = false;
        TC.enabled = false;
    }
}
