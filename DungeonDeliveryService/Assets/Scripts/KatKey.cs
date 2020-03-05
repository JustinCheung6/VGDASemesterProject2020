using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatKey : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    // Create Key Tyoes
    public enum KeyType
    {
        DefaultPackage = 0
    }

    // Returns type of key
    public KeyType GetKeyType()
    {
        return keyType;
    }
}