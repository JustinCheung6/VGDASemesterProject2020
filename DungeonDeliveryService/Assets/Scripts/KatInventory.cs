using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatInventory : MonoBehaviour
{
    private List<KatKey.KeyType> keyList;

    private void Awake()
    {
        keyList = new List<KatKey.KeyType>();
    }
    // Adds key to inventory/player
    public void AddKey(KatKey.KeyType keyType)
    {
        Debug.Log("Added key: " + keyType);
        keyList.Add(keyType);
        FindObjectOfType<Player>().increaseWeight();
    }
    //Removes key from inventory/player
    public void RemoveKey(KatKey.KeyType keyType)
    {
        keyList.Remove(keyType);
        FindObjectOfType<Player>().decreaseWeight();
    }
    // Checks if player has the correct key
    public bool ContainsKey(KatKey.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }
    // When a player collides with the door/key
    private void OnTriggerEnter2D(Collider2D collider)
    {
        KatKey key = collider.GetComponent<KatKey>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }

        KatDoor door = collider.GetComponent<KatDoor>();
        if (door != null)
        {
            if (ContainsKey(door.GetKeyType()))
            {
                RemoveKey(door.GetKeyType());
                door.OpenDoor();
            }
        }
    }
}