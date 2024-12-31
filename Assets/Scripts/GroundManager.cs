using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameObject[] groundPrefabs;
    public float zSpawn = 0;
    public float groundLength = 30;
    public int numberOfGround = 5;

    void Start()
    {
        for (int i = 0; i < numberOfGround; i++)
        { 
            if (i == 0)
            {
                SpawnGround(0);
            }
            else
            {
                SpawnGround(Random.Range(0, groundPrefabs.Length));
            }
        }
    }

    void Update()
    {
        
    }

    public void SpawnGround(int groundIndex)
    {
        Instantiate(groundPrefabs[groundIndex], transform.forward * zSpawn, transform.rotation);
        zSpawn += groundLength;
    }
}
