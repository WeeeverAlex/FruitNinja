using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spawner : MonoBehaviour
{
    private Collider2D spawnArea;

    public GameObject[] fruitPrefabs;
    public GameObject ricePrefab; 
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;
    public float minRiceSpawnDelay = 0.1f; 
    public float maxRiceSpawnDelay = 0.5f; 

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;

    private bool gameIsOver = false;

    private void Awake()
    {
        spawnArea = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (!gameIsOver)
        {
            bool spawnRice = Random.value < 0.5f; 

            GameObject prefab;
            float spawnDelay;

            if (spawnRice)
            {
                prefab = ricePrefab;
                spawnDelay = Random.Range(minRiceSpawnDelay, maxRiceSpawnDelay);
            }
            else
            {
                prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
                spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            }

            Vector2 position = new Vector2
            {
                x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                y = spawnArea.bounds.max.y
            };

            float angle = Random.Range(minAngle, maxAngle);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit, maxLifetime);

            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void EndGame()
    {
        gameIsOver = true;
    }
}
