using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import SceneManager

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f; // Start with 60 seconds
    public Text timerText; // Reference to UI Text
    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else if (timeRemaining <= 0 && !isGameOver)
        {
            EndGame();
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining); // Round up to avoid decimals
    }

    void EndGame()
    {
        isGameOver = true;
        timeRemaining = 0;
        timerText.text = "Time's Up!";
        Debug.Log("Game Over! Returning to Main Menu...");

        // Load Main Menu after 3 seconds
        Invoke("ReturnToMainMenu", 3f);
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Change to your Main Menu scene name
    }
}
