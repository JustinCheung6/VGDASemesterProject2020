using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatDoor : MonoBehaviour
{

    [SerializeField] private KatKey.KeyType keyType;
    // Gets key type
    public KatKey.KeyType GetKeyType()
    {
        return keyType;
    }

    // Door disappear
    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}

