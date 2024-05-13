using UnityEngine;

public class fruitController : MonoBehaviour
{
    public float launchPower = 10f;
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private float screenHeight;
    private bool isMovingUp = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        screenHeight = Camera.main.orthographicSize;
        startPosition = transform.position;
        LaunchFruit();
    }

    void Update()
    {
        
        if (isMovingUp && transform.position.y >= screenHeight - 1) 
        {
            rb.velocity = Vector2.zero;  
            rb.isKinematic = true;  
            isMovingUp = false;  
        }
        
        if (!isMovingUp)  
        {
            transform.position += Vector3.down * launchPower * Time.deltaTime;
            if (transform.position.y < startPosition.y)
            {
                rb.isKinematic = false;  
                LaunchFruit();  
            }
        }
    }

    void LaunchFruit()
    {
        float randomDirection = Random.Range(-1f, 1f);  
        rb.isKinematic = false;
        rb.velocity = new Vector2(randomDirection, 1) * launchPower;
        isMovingUp = true;
    }
}
