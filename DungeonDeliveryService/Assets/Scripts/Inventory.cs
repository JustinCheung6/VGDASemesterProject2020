using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Key.KeyType> keyList;

    private void Awake()
    {
        keyList = new List<Key.KeyType>();
    }
    // Adds key to inventory/player
    public void AddKey(Key.KeyType keyType)
    {
        Debug.Log("Added key: " + keyType);
        keyList.Add(keyType);
        FindObjectOfType<Player>().increaseWeight();
    }
    //Removes key from inventory/player
    public void RemoveKey(Key.KeyType keyType)
    {
        keyList.Remove(keyType);
        FindObjectOfType<Player>().decreaseWeight();
    }
    // Checks if player has the correct key
    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }
    // When a player collides with the door/key
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Key key = collider.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }

        Door door = collider.GetComponent<Door>();
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