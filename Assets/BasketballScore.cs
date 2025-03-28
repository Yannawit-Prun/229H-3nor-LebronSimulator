using UnityEngine;

public class BasketballScore : MonoBehaviour
{
    public int scoreValue = 1; // Points per basket

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // Make sure the basketball has the tag "Basketball"
        {
            BasketballShooting shootingScript = FindObjectOfType<BasketballShooting>();
            if (shootingScript != null)
            {
                shootingScript.AddScore(scoreValue); // Add points
            }
        }
    }
}
