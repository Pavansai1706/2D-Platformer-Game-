using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float moveDistance = 10f; // Distance to move forth and back

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingForward = true;
    private Vector3 originalScale;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.right * moveDistance;
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Move the enemy back and forth
        if (movingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                movingForward = false;
                // Flip the enemy's scale to change direction
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                movingForward = true;
                // Flip the enemy's scale back to original direction
                transform.localScale = originalScale;
            }
        }
    }
}
