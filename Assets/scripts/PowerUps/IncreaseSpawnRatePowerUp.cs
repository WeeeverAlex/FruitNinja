using System.Collections;
using UnityEngine;

public class IncreaseSpawnRatePowerUp : PowerUp
{
    public float spawnIntervalMultiplier = 0.1f; 
    public AudioClip powerUpSound;
    public AudioSource audioSource;

    protected override void ActivatePowerUp()
    {
        StartCoroutine(PowerUpEffect());
    }

    protected override IEnumerator PowerUpEffect()
    {
        
        if (audioSource != null && powerUpSound != null)
        {
            audioSource.PlayOneShot(powerUpSound);
        }

        
        Spawner spawner = FindObjectOfType<Spawner>();
        if (spawner != null)
        {
            
            float originalMinSpawnDelay = spawner.minSpawnDelay;
            float originalMaxSpawnDelay = spawner.maxSpawnDelay;

            
            spawner.minSpawnDelay *= spawnIntervalMultiplier;
            spawner.maxSpawnDelay *= spawnIntervalMultiplier;

            
            yield return new WaitForSeconds(duration);

            
            spawner.minSpawnDelay = originalMinSpawnDelay;
            spawner.maxSpawnDelay = originalMaxSpawnDelay;
        }

        DeactivatePowerUp();
    }

    protected override void DeactivatePowerUp()
    {
        base.DeactivatePowerUp();
    }
}
