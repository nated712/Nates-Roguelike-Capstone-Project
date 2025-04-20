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

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void takeDamage(float dmg){
        currentHealth -= dmg;
        Instantiate(hurtEffect, transform.position, Quaternion.identity);
        if(currentHealth <= 0){
            Kill();
        }
    }

    void Kill(){
        //add an on kill event
        Instantiate(killEffect, transform.position, Quaternion.identity);
        int roll = Random.Range(0, 21);
        if (roll == 20){
            Instantiate(HPDrop, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
