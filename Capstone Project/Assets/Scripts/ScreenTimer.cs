using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to TMP UI Text
    public float elapsedTime = 0f;
    private bool isRunning = false;
    private bool isTimerStopped = false; // Flag to check if the timer has been stopped
    private float timeReductionAmount = 5f; // Amount of time to reduce (in seconds)

    void Start()
    {
        isRunning = true; // Start the timer automatically
    }

    void Update()
    {
        if (isRunning && !isTimerStopped)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        PersistentData.Instance.timer = timerText.text;
    }

    public void StopTimer()
    {
        isRunning = false;
        isTimerStopped = true; // Mark the timer as stopped so it can't be started again
    }

    public void StartTimer()
    {
        if (!isTimerStopped) // Check if the timer has been stopped previously
        {
            isRunning = true;
        }
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerDisplay();
        isTimerStopped = false; // Allow the timer to be started again after reset
        isRunning = true; // Start the timer again after reset
    }

    // Method to reduce time when all turrets are destroyed
    public void ReduceTime()
    {
        elapsedTime -= timeReductionAmount; // Reduce the time by a specified amount
        if (elapsedTime < 0) elapsedTime = 0f; // Ensure time doesn't go negative
        UpdateTimerDisplay();
    }

    public string ReturnTimerText(float TimerFloat)
    {
        int minutes = Mathf.FloorToInt(TimerFloat / 60);
        int seconds = Mathf.FloorToInt(TimerFloat % 60);
        string returnText = string.Format("{0:00}:{1:00}", minutes, seconds);
        return returnText;
    }

    public float ReturnElapsedTime()
    {
        return elapsedTime;
    }
}
