using UnityEngine;

public class SwipeToCut : MonoBehaviour
{
    private Camera cam;
    private bool isDragging = false;
    private Vector2 lastPosition;

    void Start()
    {
        cam = Camera.main; 
    }

    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isDragging = true;
                    lastPosition = cam.ScreenToWorldPoint(touchPos);
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                        CheckCut(touchPos);
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }

        
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            CheckCut(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void CheckCut(Vector2 position)
    {
        Vector3 currentPosition = cam.ScreenToWorldPoint(position);
        currentPosition.z = 0; 

        
        if (Vector3.Distance(lastPosition, currentPosition) > 0.1f) 
        {
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Fruit"))
            {
                Destroy(hit.collider.gameObject); 
            }
            lastPosition = currentPosition;
        }
    }
}
