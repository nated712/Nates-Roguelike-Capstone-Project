using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;

        if (scoreManager == null)
        {
            scoreManager = FindFirstObjectByType<ScoreManager>();
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

        int roll = Random.Range(0, 21); // 0 to 20

        if (roll == 20)
        {
            Instantiate(HPDrop, transform.position, Quaternion.identity);
        }
        else //if (roll == 19)
        {
            Instantiate(weaponUpgradeDrop, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
