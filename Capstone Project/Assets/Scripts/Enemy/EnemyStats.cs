using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private float currentMoveSpeed;
    private float currentHealth;
    private float currentDamage;
    [SerializeField] public GameObject hurtEffect;
    [SerializeField] public GameObject killEffect;
    [SerializeField] public GameObject HPDrop;
    [SerializeField] public GameObject weaponUpgradeDrop;
    public ScoreManager scoreManager;
    private GameObject player;
    
    private static int totalKillCount = 0; // track total kills across all enemies
    private int increaseKillsforGuaranteedUpgrade = 5;
    private int requiredKills = 5;

    [SerializeField] private Slider upgradeProgressSlider; 
    int xpValue = 0; // XP value to track progress per enemy
    
    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;

        if (scoreManager == null)
        {
            scoreManager = FindFirstObjectByType<ScoreManager>();
        }

        player = GameObject.FindWithTag("Player");

        // Ensure upgradeProgressSlider is found and initialized
        upgradeProgressSlider = GameObject.Find("XPbar")?.GetComponent<Slider>();
        if (upgradeProgressSlider != null)
        {
            upgradeProgressSlider.maxValue = requiredKills;
            upgradeProgressSlider.value = totalKillCount % requiredKills; // Update based on total kills
        }
    }

    public void takeDamage(float dmg)
    {
        currentHealth -= dmg;
        Instantiate(hurtEffect, transform.position, Quaternion.identity);
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        Instantiate(killEffect, transform.position, Quaternion.identity);
        scoreManager.AddScore(100f);
        
        totalKillCount++; // Track total kills across all enemies
        xpValue = totalKillCount % requiredKills; // Update xp value based on total kills
        UpdateProgressBar();

        // Check for upgrade drop based on total kill count
        if (totalKillCount >= requiredKills)
        {
            Instantiate(weaponUpgradeDrop, player.transform.position, Quaternion.identity);
            totalKillCount = 0; // Reset after upgrade drop
            requiredKills += increaseKillsforGuaranteedUpgrade; // Increase required kills for next drop
            upgradeProgressSlider.maxValue = requiredKills;
        }

        int roll = Random.Range(0, 31); // Random chance Drops
        if (roll == 20)
        {
            Instantiate(weaponUpgradeDrop, transform.position, Quaternion.identity);
        }
        if (roll == 30)
        {
            Instantiate(HPDrop, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    void UpdateProgressBar()
    {
        if (upgradeProgressSlider != null)
        {
            upgradeProgressSlider.value = xpValue; // Update based on total kill count
        }
    }
}
