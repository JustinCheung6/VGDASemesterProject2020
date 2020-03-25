using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private Key.KeyType keyType;
    // Gets key type
    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    // Door disappear
    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}

