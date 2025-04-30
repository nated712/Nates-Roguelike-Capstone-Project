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

    void Start()
    {
        if (enemyData == null)
        {
            Debug.LogError("EnemyScriptableObject is not assigned.");
            return;
        }

        // Auto-assign UI if not set in Inspector
        if (bossCanvas == null)
            bossCanvas = GameObject.Find("BossCanvas")?.GetComponent<Canvas>();

        if (healthSlider == null)
            healthSlider = GameObject.Find("BossHealthSlider")?.GetComponent<Slider>();

        if (bossNameText == null)
            bossNameText = GameObject.Find("BossNameText")?.GetComponent<TextMeshProUGUI>();

        if (bossCanvas == null || healthSlider == null || bossNameText == null)
        {
            Debug.LogError("Boss UI references are missing.");
            return;
        }

        currentHealth = enemyData.MaxHealth;
        healthSlider.maxValue = enemyData.MaxHealth;
        healthSlider.value = currentHealth;
        bossNameText.text = bossName;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, enemyData.MaxHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            bossCanvas.enabled = false;
        }
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
