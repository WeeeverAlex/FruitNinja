using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PowerUpSpawner : MonoBehaviour
{
    private BoxCollider2D spawnArea;

    public GameObject[] powerUpPrefabs;
    public float minPowerUpSpawnDelay = 5f; 
    public float maxPowerUpSpawnDelay = 10f; 

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minPowerUpForce = 18f;
    public float maxPowerUpForce = 22f;

    public float maxLifetime = 5f;

    private bool gameIsOver = false;
    private HashSet<string> activePowerUps = new HashSet<string>();
    private Dictionary<string, float> powerUpCooldowns = new Dictionary<string, float>();

    private void Awake()
    {
        spawnArea = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(ManagePowerUpSpawning());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator ManagePowerUpSpawning()
    {
        while (!gameIsOver)
        {
            foreach (var powerUpPrefab in powerUpPrefabs)
            {
                string powerUpName = powerUpPrefab.name;

                if (!activePowerUps.Contains(powerUpName))
                {
                    if (!powerUpCooldowns.ContainsKey(powerUpName))
                    {
                        float spawnDelay = Random.Range(minPowerUpSpawnDelay, maxPowerUpSpawnDelay);
                        powerUpCooldowns[powerUpName] = Time.time + spawnDelay;
                    }
                    else if (Time.time >= powerUpCooldowns[powerUpName])
                    {
                        SpawnObject(powerUpPrefab);
                        activePowerUps.Add(powerUpName);
                        powerUpCooldowns.Remove(powerUpName);
                    }
                }
            }
            yield return new WaitForSeconds(1f); 
        }
    }

    private void SpawnObject(GameObject prefab)
    {
        Vector2 position = new Vector2
        {
            x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            y = spawnArea.bounds.max.y
        };

        float angle = Random.Range(minAngle, maxAngle);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject obj = Instantiate(prefab, position, rotation);
        Destroy(obj, maxLifetime);

        float force = Random.Range(minPowerUpForce, maxPowerUpForce);
        obj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    public void DeactivatePowerUp(string powerUpName)
    {
        if (activePowerUps.Contains(powerUpName))
        {
            activePowerUps.Remove(powerUpName);
            float spawnDelay = Random.Range(minPowerUpSpawnDelay, maxPowerUpSpawnDelay);
            powerUpCooldowns[powerUpName] = Time.time + spawnDelay;
        }
    }

    public void EndGame()
    {
        gameIsOver = true;
    }
}
