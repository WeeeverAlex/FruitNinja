using System.Collections;
using UnityEngine;

public class SlowMotionPowerUp : PowerUp
{
    public float slowMotionFactor = 0.5f;
    public AudioClip powerUpSound;
    public AudioSource audioSource;

    protected override void ActivatePowerUp()
    {
        Time.timeScale = slowMotionFactor;

        
        if (audioSource != null && powerUpSound != null)
        {
            audioSource.PlayOneShot(powerUpSound);
        }
    }

    protected override IEnumerator PowerUpEffect()
    {
        ActivatePowerUp();
        yield return new WaitForSecondsRealtime(duration);
        DeactivatePowerUp();
    }

    protected override void DeactivatePowerUp()
    {
        Time.timeScale = 1f;
        base.DeactivatePowerUp();
    }
}
