using System.Collections;
using UnityEngine;

public class CompleteAllDishesPowerUp : PowerUp
{
    protected override void ActivatePowerUp()
    {
        DishManager dishManager = FindObjectOfType<DishManager>();
        if (dishManager != null)
        {
            dishManager.CompleteAllDishes();
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
