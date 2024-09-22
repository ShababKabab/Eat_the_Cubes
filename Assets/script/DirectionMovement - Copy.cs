using UnityEngine;

public class DirectionMovement : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float directionChangeInterval;
    private float timeSinceDirectionChange;
    private float planeSize;  // Add planeSize as a field
    private GameObject player;  // Reference to the player cube
    private Rigidbody rb; // Declare the Rigidbody variable
    public float detectionRadius = 50f; // Radius to detect the player cube
    public LayerMask playerLayer; // Layer to identify the player

    public void Initialize(float moveSpeed, float changeInterval, float planeSize, GameObject playerCube)
    {
        speed = moveSpeed;
        rb = GetComponent<Rigidbody>(); 
        directionChangeInterval = changeInterval;
        this.planeSize = planeSize;
        this.player = playerCube;  // Store player cube reference
        timeSinceDirectionChange = 0f;
        SetNewDirection();
    }

    void FixedUpdate()
    {
        timeSinceDirectionChange += Time.deltaTime;

        // Check for the player and chase/run away if necessary
        if (DetectAndChasePlayer())
        {
            // Player detected, change direction to run away
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            return; // Skip random movement if chasing
        }

        // Regular random movement
        if (timeSinceDirectionChange >= directionChangeInterval)
        {
            SetNewDirection();
            timeSinceDirectionChange = 0f;
        }

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        // Ensure the cube stays within the plane boundaries
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -planeSize / 2, planeSize / 2);
        position.z = Mathf.Clamp(position.z, -planeSize / 2, planeSize / 2);
        transform.position = position;
    }

    private bool DetectAndChasePlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        if (hits.Length > 0)
        {
            // If the player is detected, run away
            Transform playerTransform = hits[0].transform;
            Vector3 awayFromPlayer = (transform.position - playerTransform.position).normalized;
            direction = awayFromPlayer; // Set the direction to move away from the player
            return true; // Return true to indicate the player was detected
        }

        return false; // Return false if no player is detected
    }
    private void SetNewDirection()
    {
        direction = new Vector3(
            Random.Range(-1f, 1f),
            0f,
            Random.Range(-1f, 1f)
        ).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is a cube
        if (collision.gameObject.CompareTag("Cube"))
        {
            // Compare sizes of the colliding cubes
            float thisScale = transform.localScale.x;
            float otherScale = collision.transform.localScale.x;

            if (thisScale < otherScale)
            {
                // Grow the bigger cube
                collision.transform.localScale += Vector3.one * 0.10f; // Increase the scale by 0.10
                // Destroy the smaller cube
                Destroy(gameObject);
            }
        }
    }
}