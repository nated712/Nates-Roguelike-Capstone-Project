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
    private XPManager xpManager;  
    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;

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
        Instantiate(hurtEffect, transform.position, Quaternion.identity);
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        Instantiate(killEffect, transform.position, Quaternion.identity);
        scoreManager.AddScore(145f);

        if (xpManager != null)
        {
            xpManager.RegisterEnemyKill(); // tell XP Manager 
        }

        int roll = Random.Range(0, 31); // Random chance Drops
        if (roll != 20)
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
