using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public float score;
    public TextMeshProUGUI scoreText; // Reference to TMP UI Text
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0f;
        scoreText.text = score.ToString();
    }

    public void AddScore(float scoreToAdd){
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }
}
