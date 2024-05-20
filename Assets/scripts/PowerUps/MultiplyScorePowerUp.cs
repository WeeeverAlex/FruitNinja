using System.Collections;
using UnityEngine;

public class MultiplyScorePowerUp : PowerUp
{
    public int scoreMultiplier = 2;

    protected override void ActivatePowerUp()
    {
        gameManager.SetScoreMultiplier(scoreMultiplier);
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
