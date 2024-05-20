using System.Collections;
using UnityEngine;

public class SlowMotionPowerUp : PowerUp
{
    public float slowMotionFactor = 0.5f;

    protected override void ActivatePowerUp()
    {
        Time.timeScale = slowMotionFactor;
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
