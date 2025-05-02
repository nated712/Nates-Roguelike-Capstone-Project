using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Canvas bossCanvas;
    public Slider healthSlider;
    public TextMeshProUGUI bossNameText;

    [Header("Boss Data")]
    public string bossName = "Boss";
    public EnemyScriptableObject enemyData; // Reference to ScriptableObject
    private float currentHealth;
    private float maxHealth;    
    public Timer screenTimer;
    public float healthScaling = 3f;

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);


        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 1)
        {
            HideUI();
        }
    }

    public void InitializeHealth(float startHealth, float fullHealth, string name)
    {
        currentHealth = startHealth;
        maxHealth = fullHealth;

        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("BossHealthSlider")?.GetComponent<Slider>();
        }
        if (bossNameText == null)
        {
            bossNameText = GameObject.Find("BossNameText")?.GetComponent<TextMeshProUGUI>();
        }
        if (bossCanvas == null)
        {
            bossCanvas = GameObject.Find("BossCanvas")?.GetComponent<Canvas>();
        }

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (bossNameText != null)
        {
            bossNameText.text = name;
        }

        ShowUI();
    }

    public void ShowUI()
    {
        bossCanvas.enabled = true;
    }

    public void HideUI()
    {
        bossCanvas.enabled = false;
    }
}
