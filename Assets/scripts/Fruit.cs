using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;
    public string ingredientName;

    private Rigidbody2D fruitRigidbody;
    private Collider2D fruitCollider;
    private DishManager dishManager;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody2D>();
        fruitCollider = GetComponent<Collider2D>();
        dishManager = FindObjectOfType<DishManager>();
    }

    private void Slice(Vector2 direction, Vector2 position, float force)
    {
        fruitCollider.enabled = false;
        whole.SetActive(false);
        sliced.SetActive(true);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody2D[] slices = sliced.GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D slice in slices)
        {
            if (slice != null)
            {
                slice.velocity = fruitRigidbody.velocity;
                slice.AddForceAtPosition(direction * force, position, ForceMode2D.Impulse);
            }
        }

        if (dishManager != null)
        {
            dishManager.IngredientSliced(ingredientName);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.Direction, blade.transform.position, blade.sliceForce);
            VibrationManager.Vibrate(100); // Chame o método estático diretamente
        }
    }
}
