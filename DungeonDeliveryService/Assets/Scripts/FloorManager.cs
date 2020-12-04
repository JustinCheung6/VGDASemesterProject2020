using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    private static FloorManager singleton = null;
    public static FloorManager Get { get => singleton; }

    private List<Collider2D> floor1Colliders = new List<Collider2D>();
    private List<Collider2D> floor2Colliders = new List<Collider2D>();
    private List<SnowPile> snowPiles = new List<SnowPile>();


    [Tooltip("True if starting upstairs, False if starting downstairs")]
    [SerializeField] private bool upStairs;
    public bool UpStairs { get => upStairs; }

    private void Awake()
    {
        if(FloorManager.singleton == null)
        {
            //Debug.Log("FloorManager Set");
            FloorManager.singleton = this;
        }
        else if(FloorManager.singleton != this)
        {
            Debug.Log("Extra singleton found for FloorManager. " + this);
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        GameObject[] floor1 = GameObject.FindGameObjectsWithTag("Floor1");
        foreach(GameObject f in floor1)
        {
            if (f.GetComponent<Collider2D>() != null)
                floor1Colliders.Add(f.GetComponent<Collider2D>());
            else
                Debug.Log("Floor1. Collider2D not found");
        }
        GameObject[] floor2 = GameObject.FindGameObjectsWithTag("Floor2");
        foreach (GameObject f in floor2)
        {
            if (f.GetComponent<SnowPile>() != null)
                snowPiles.Add(f.GetComponent<SnowPile>());
            else if (f.GetComponent<Collider2D>() != null)
                floor2Colliders.Add(f.GetComponent<Collider2D>());
            else
                Debug.Log("Floor2. Collider2D not found");
        }


        if (upStairs)
            Upstairs();
        else
            Downstairs();
    }

    public void Upstairs()
    {
        //Debug.Log("Going Up");
        foreach(Collider2D f in floor1Colliders)
        {
            f.enabled = false;
        }
        foreach(Collider2D f in floor2Colliders)
        {
            f.enabled = true;
        }
        foreach(SnowPile s in snowPiles)
        {
            s.SetStairs(true);
        }

        upStairs = true;
    }
    public void Downstairs()
    {
        //Debug.Log("Going Down");
        foreach (Collider2D f in floor1Colliders)
        {
            f.enabled = true;
        }
        foreach (Collider2D f in floor2Colliders)
        {
            f.enabled = false;
        }
        foreach (SnowPile s in snowPiles)
        {
            s.SetStairs(false);
        }

        upStairs = false;
    }
}
