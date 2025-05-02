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
    [SerializeField] public float enemyScoreValue;
    [SerializeField] public int enemyKillValue;
    public ScoreManager scoreManager;  
    private XPManager xpManager;  
    public BossUI bossUI; // Reference to Boss UI
    public Timer screenTimer;
    public float healthScaling = .01f;
    void Awake()
    {
        if(screenTimer == null){
            screenTimer = FindFirstObjectByType<Timer>();
        }
        currentMoveSpeed = enemyData.MoveSpeed;
        //Some health scaling!!
        currentHealth = enemyData.MaxHealth + (screenTimer.elapsedTime * healthScaling);
        currentDamage = enemyData.Damage;
        // Initialize Boss UI health if boss
        if (bossUI != null)
        {
            bossUI.InitializeHealth(currentHealth, enemyData.MaxHealth + (screenTimer.elapsedTime * healthScaling), "Wendell, Undying Lord of the Goblin Horde");
        }
        if (scoreManager == null)
        {
            scoreManager = FindFirstObjectByType<ScoreManager>();
        }
        xpManager = FindFirstObjectByType<XPManager>(); 
        
    }

    public void takeDamage(float dmg)
    {
        scoreManager.AddScore(5f);
        currentHealth -= dmg;

        if (bossUI != null)
        {
            bossUI.TakeDamage(dmg);  // Update the health in BossUI
        }
        
        Instantiate(hurtEffect, transform.position, Quaternion.identity);
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        Instantiate(killEffect, transform.position, Quaternion.identity);
        scoreManager.AddScore(enemyScoreValue);

        if (xpManager != null)
        {
            xpManager.GainXP(enemyKillValue); // tell XP Manager 
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
}
