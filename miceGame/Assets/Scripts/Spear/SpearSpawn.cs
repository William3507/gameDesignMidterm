using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSpawn : MonoBehaviour
{
    public float spawnRate = 10f;
    public GameObject spearPrefab;
    private float lastSpawnTime = 0;

    void Update()
    {
        if (lastSpawnTime + 1 / spawnRate < Time.time)
        {
            lastSpawnTime = Time.time;
            Vector3 spawnPosition = transform.position;
            Instantiate(spearPrefab, spawnPosition, Quaternion.identity);
            
        }
    }
}
