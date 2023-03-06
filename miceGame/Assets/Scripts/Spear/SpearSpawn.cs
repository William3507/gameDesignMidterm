using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSpawn : MonoBehaviour
{
    public float spawnRate = 0.5f;
    public GameObject spearPrefab;

    private float lastSpawnTime = 0;

    void Update()
    {
        if (lastSpawnTime + 1 / spawnRate < Time.time)
        {
            lastSpawnTime = Time.time;
            Vector3 spawnPosition = transform.position;
            
            // the Instatiate function creates a new GameObject copy (clone) from a Prefab at a specific location and orientation.
            Instantiate(spearPrefab, spawnPosition, Quaternion.identity);
            
        }
    }
}
