using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPile : MonoBehaviour
{

    public GameObject Floor1;
    public GameObject Floor2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            Floor1.SetActive(true);
            Floor2.SetActive(false);
        }
    }
}
