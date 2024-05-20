using System.Collections;
using UnityEngine;

public class IncreaseSpawnRatePowerUp : PowerUp
{
    public float increasedRate = 0.1f;

    protected override void ActivatePowerUp()
    {
        StartCoroutine(PowerUpEffect());
    }

    protected override IEnumerator PowerUpEffect()
    {
        float originalMinSpawnDelay = FindObjectOfType<Spawner>().minSpawnDelay;
        float originalMaxSpawnDelay = FindObjectOfType<Spawner>().maxSpawnDelay;

        FindObjectOfType<Spawner>().minSpawnDelay = increasedRate;
        FindObjectOfType<Spawner>().maxSpawnDelay = increasedRate;

        yield return new WaitForSeconds(duration);

        FindObjectOfType<Spawner>().minSpawnDelay = originalMinSpawnDelay;
        FindObjectOfType<Spawner>().maxSpawnDelay = originalMaxSpawnDelay;

        DeactivatePowerUp();
    }

    protected override void DeactivatePowerUp()
    {
        base.DeactivatePowerUp();
    }
}
