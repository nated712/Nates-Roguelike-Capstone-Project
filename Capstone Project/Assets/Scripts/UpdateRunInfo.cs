using UnityEngine;
using TMPro;
public class UpdateRunInfo : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PersistentData.Instance != null){
            scoreText.text = PersistentData.Instance.score.ToString();
            timerText.text = PersistentData.Instance.timer; 
            if(PersistentData.Instance.highScore < PersistentData.Instance.score)
            {
                PersistentData.Instance.highScore = PersistentData.Instance.score;
            }
            highScoreText.text = PersistentData.Instance.highScore.ToString();
            
        }
    }
}

