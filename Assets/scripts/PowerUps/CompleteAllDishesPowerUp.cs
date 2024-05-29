using System.Collections;
using UnityEngine;

public class CompleteAllDishesPowerUp : PowerUp
{
    public AudioClip powerUpSound;
    public AudioSource audioSource;

    protected override void ActivatePowerUp()
    {
        DishManager dishManager = FindObjectOfType<DishManager>();
        if (dishManager != null)
        {
            dishManager.CompleteAllDishes();
        }

        
        if (audioSource != null && powerUpSound != null)
        {
            audioSource.PlayOneShot(powerUpSound);
        }
    }

    protected override IEnumerator PowerUpEffect()
    {
        ActivatePowerUp();
        yield return new WaitForSeconds(duration);
        DeactivatePowerUp();
    }

    protected override void DeactivatePowerUp()
    {
        base.DeactivatePowerUp();
    }
}
