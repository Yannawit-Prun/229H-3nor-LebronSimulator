using UnityEngine;
using UnityEngine.UI;

public class BasketballShooting : MonoBehaviour
{
    public GameObject basketballPrefab;
    public Transform shootPoint;
    public float mass = 0.6f;
    public float baseAcceleration = 20f;
    public float maxAcceleration = 50f;
    public float chargeRate = 10f;
    private float currentAcceleration;
    public Transform[] randomPositions;
    public Camera mainCamera;
    private bool isCharging;
    public Image chargeBarFill;

    public float ballLifetime = 10f;
    public float velocityThreshold = 0.2f;
    public float checkDelay = 2f;

    public Text scoreText; // UI Text to display the score
    private int score = 0; // Player's score

    void Start()
    {
        MoveShootPoint();
        currentAcceleration = baseAcceleration;
        UpdateChargeGauge();
        UpdateScoreUI(); // Initialize score UI
    }

    void Update()
    {
        AimWithMouse();

        if (Input.GetButtonDown("Fire1"))
        {
            isCharging = true;
            currentAcceleration = baseAcceleration;
        }

        if (Input.GetButton("Fire1"))
        {
            currentAcceleration += chargeRate * Time.deltaTime;
            currentAcceleration = Mathf.Clamp(currentAcceleration, baseAcceleration, maxAcceleration);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            ShootBall();
            MoveShootPoint();
            isCharging = false;
            currentAcceleration = baseAcceleration;
        }

        UpdateChargeGauge();
    }

    void MoveShootPoint()
    {
        if (randomPositions.Length > 0)
        {
            int index = Random.Range(0, randomPositions.Length);
            shootPoint.position = randomPositions[index].position;
        }
    }

    void AimWithMouse()
    {
        if (mainCamera == null)
            return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (hit.point - shootPoint.position).normalized;
            shootPoint.forward = direction;
        }
    }

    void ShootBall()
    {
        if (basketballPrefab != null && shootPoint != null)
        {
            GameObject ball = Instantiate(basketballPrefab, shootPoint.position, Quaternion.identity);
            Rigidbody rb = ball.GetComponent<Rigidbody>();

            if (rb != null)
            {
                float force = mass * currentAcceleration;
                Vector3 shootingDirection = shootPoint.forward + Vector3.up * 0.5f;
                rb.AddForce(shootingDirection.normalized * force, ForceMode.Impulse);

                StartCoroutine(SelfDestruct(ball, rb));
            }
        }
    }

    void UpdateChargeGauge()
    {
        if (chargeBarFill != null)
        {
            float fillAmount = (currentAcceleration - baseAcceleration) / (maxAcceleration - baseAcceleration);
            chargeBarFill.fillAmount = fillAmount;
            chargeBarFill.gameObject.SetActive(isCharging);
        }
    }

    System.Collections.IEnumerator SelfDestruct(GameObject ball, Rigidbody rb)
    {
        float timer = 0f;

        while (timer < ballLifetime)
        {
            yield return new WaitForSeconds(checkDelay);

            if (rb.velocity.magnitude < velocityThreshold)
            {
                Destroy(ball);
                yield break;
            }

            timer += checkDelay;
        }

        Destroy(ball);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
