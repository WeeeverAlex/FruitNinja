using System.Collections;
using UnityEngine;

public class MultiplyScorePowerUp : PowerUp
{
    public int scoreMultiplier = 2;
    public AudioClip powerUpSound;
    public AudioSource audioSource;

    protected override void ActivatePowerUp()
    {
        gameManager.SetScoreMultiplier(scoreMultiplier);

        
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
        gameManager.SetScoreMultiplier(1);
        base.DeactivatePowerUp();
    }
}
