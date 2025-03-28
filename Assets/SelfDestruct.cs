using UnityEngine;

public class BasketballSelfDestruct : MonoBehaviour
{
    public float lifetime = 10f; // Max lifetime before auto-destroy
    public float velocityThreshold = 0.2f; // Speed at which ball is considered "stopped"
    public float checkDelay = 2f; // Time before checking if the ball has stopped

    private Rigidbody rb;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(CheckForDestruction), checkDelay); // Start checking after delay
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject); // Destroy after max lifetime
        }
    }

    void CheckForDestruction()
    {
        if (rb != null && rb.velocity.magnitude < velocityThreshold)
        {
            Destroy(gameObject); // Destroy if the ball has stopped moving
        }
        else
        {
            Invoke(nameof(CheckForDestruction), checkDelay); // Check again later
        }
    }

}
