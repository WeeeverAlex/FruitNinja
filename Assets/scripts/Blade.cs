using UnityEngine;

public class Blade : MonoBehaviour
{
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private Camera mainCamera;
    private Collider2D sliceCollider;
    private TrailRenderer sliceTrail;

    private Vector2 direction;
    public Vector2 Direction => direction;

    private bool slicing;
    public bool Slicing => slicing;

    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider2D>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlice();
    }

    private void OnDisable()
    {
        StopSlice();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                StartSlice(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                StopSlice();
            }
            else if (slicing && touch.phase == TouchPhase.Moved)
            {
                ContinueSlice(touch.position);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            StartSlice(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlice();
        }
        else if (slicing && Input.GetMouseButton(0))
        {
            ContinueSlice(Input.mousePosition);
        }
    }

    private void StartSlice(Vector2 screenPosition)
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, mainCamera.nearClipPlane));
        position.z = 0f;
        transform.position = position;

        slicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
        sliceTrail.Clear();
    }

    private void StopSlice()
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlice(Vector2 screenPosition)
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, mainCamera.nearClipPlane));
        newPosition.z = 0f;
        direction = new Vector2(newPosition.x - transform.position.x, newPosition.y - transform.position.y);

        float velocity = direction.magnitude / Time.deltaTime;
        sliceCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }
}

