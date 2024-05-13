using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
public GameObject fruitPrefab; 
    public float spawnRate = 2.0f; 
    private float nextTimeToSpawn = 0.0f;
    private float screenWidth;

    void Start()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    void Update()
    {
        if (Time.time >= nextTimeToSpawn)
        {
            SpawnFruit();
            nextTimeToSpawn = Time.time + spawnRate;
        }
    }

    void SpawnFruit()
    {
        float randomX = Random.Range(-screenWidth, screenWidth);
        Vector3 spawnPosition = new Vector3(randomX, -Camera.main.orthographicSize - 1, 0);
        
        
        Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }
}
