using System.Collections;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;
    public string powerUpName;
    public float duration = 10f; 
    public float cooldown = 30f; 

    private Rigidbody2D powerUpRigidbody;
    private Collider2D powerUpCollider;
    protected GameManager gameManager;
    protected PowerUpSpawner spawner; 

    public delegate void PowerUpDeactivated();
    public event PowerUpDeactivated OnPowerUpDeactivated;

    private bool isSliced = false;

    private void Awake()
    {
        powerUpRigidbody = GetComponent<Rigidbody2D>();
        powerUpCollider = GetComponent<Collider2D>();
        gameManager = FindObjectOfType<GameManager>();
        spawner = FindObjectOfType<PowerUpSpawner>(); 
    }

    public void Initialize(PowerUpSpawner spawner, string powerUpName, float maxLifetime)
    {
        this.spawner = spawner;
        this.powerUpName = powerUpName;
        StartCoroutine(DestroyAfterTime(maxLifetime));
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (!isSliced)
        {
            OnPowerUpDeactivated?.Invoke();
            Destroy(gameObject);
        }
    }

    private void Slice(Vector2 direction, Vector2 position, float force)
    {
        if (isSliced) return;

        isSliced = true;
        powerUpCollider.enabled = false;
        whole.SetActive(false);
        sliced.SetActive(true);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody2D[] slices = sliced.GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D slice in slices)
        {
            if (slice != null)
            {
                slice.velocity = powerUpRigidbody.velocity;
                slice.AddForceAtPosition(direction * force, position, ForceMode2D.Impulse);
            }
        }

        Debug.Log($"Power-up {powerUpName} activated.");
        StartCoroutine(PowerUpEffect());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.Direction, blade.transform.position, blade.sliceForce);
        }
    }

    protected abstract void ActivatePowerUp();

    protected virtual IEnumerator PowerUpEffect()
    {
        ActivatePowerUp();
        yield return new WaitForSeconds(duration);
        DeactivatePowerUp();
    }

    protected virtual void DeactivatePowerUp()
    {
        Debug.Log($"Power-up {powerUpName} deactivated after {duration} seconds.");
        OnPowerUpDeactivated?.Invoke();
        Destroy(gameObject);
    }
}
