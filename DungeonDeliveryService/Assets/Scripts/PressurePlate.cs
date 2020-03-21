using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private int weight = 100;
    [SerializeField] private int weight_threshold = 200;

    [SerializeField] bool isOpened = false;
    [SerializeField] private BoxCollider2D BC;
    [SerializeField] private SpriteRenderer SR;
    private void OnTriggerEnter2D(Collider2D PressurePlate)
    {
        
        if (!isOpened)
        {
            isOpened = true;
            BC.enabled = false;
            SR.enabled = false;
        }
    }
}
