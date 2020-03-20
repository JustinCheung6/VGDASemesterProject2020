using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private void Start()
    {
        weight = 100;
        weight_threshold = 200
    }
    
    [SerializeField]
    GameObject PressurePlate;

    bool isOpened = false;

    private void OnTriggerEnter(Collider PressurePlate)
    {
        if (isOpened)
        {
            isOpened = true;
            if (weight)
            {
                weight = weight_threshold;
                PressurePlate.transform.position += new Vector3(10, 0, 0);
            }
            
        }
    }
}
