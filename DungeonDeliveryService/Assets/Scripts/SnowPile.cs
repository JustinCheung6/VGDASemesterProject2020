using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPile : MonoBehaviour
{
    /* im still confused on how unity stuff works
     * im just basing this off if we automatically
     * make snowtiles a tilecollider or something
     * even then code be wonky as i dont know how
     * to check if floor is upstairs or downstairs */

    /*public GameObject Floor1;
    public GameObject Floor2;*/

    [SerializeField] private bool snowpile;
    [SerializeField] private bool upstairs;
    [SerializeField] private bool downstairs;

    // Start is called before the first frame update
    void Start()
    {
        if(upstairs)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        /*if (player.gameObject.tag == "Player")
        {
            Floor1.SetActive(true);
            Floor2.SetActive(false);
        }*/

        if(snowpile)
        {
            if(upstairs)
            {
                FloorManager.singleton.Downstairs();
                Destroy(gameObject);
            }
        }
    }
}
