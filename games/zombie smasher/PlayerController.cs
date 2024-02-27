using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text timerText; // Reference to a UI Text component for displaying the timer.
    public Text scoreText; // Reference to a UI Text component for displaying the score.
    public float initialGameDuration = 60.0f; // Initial total game duration in seconds.
    public float timePerScorePoint = 5.0f; // Amount of time to add per score point.

    private float gameDuration;
    private float currentTime;
    private int score;

    private void Start()
    {
        gameDuration = initialGameDuration;
        currentTime = gameDuration;
        score = 0;
        UpdateTimerUI();
        UpdateScoreUI();
    }

    private void Update()
    {
        // Update the timer and display it on the UI.
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            // Game over logic (e.g., show the game over screen).
            GameOver();
        }

        UpdateTimerUI();
        UpdateScoreUI();

        // Check for user input to move the car.
        // Add your car movement code here.
    }

    public void IncreaseScore()
    {
        score += 1;
        UpdateScoreUI();

        // Increase game duration based on the score.
        currentTime += score * timePerScorePoint;
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + currentTime.ToString("0");
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void GameOver()
    {
        // Implement your game over logic here, such as displaying a game over screen.
        // You can also stop the car's movement or perform any other actions.
    }
}
