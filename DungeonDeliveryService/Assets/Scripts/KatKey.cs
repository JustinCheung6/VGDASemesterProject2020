using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatKey : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    // Create Key Tyoes
    public enum KeyType
    {
        ChestKey,
        DoorKey
    }

    // Returns type of key
    public KeyType GetKeyType()
    {
        return keyType;
    }
}