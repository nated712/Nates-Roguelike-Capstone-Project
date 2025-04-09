using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    public float projSpeed = 15f;
    public float destroyTime = 5f;
    public float damage = 2f;
    


    [SerializeField] private int pierce;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDestroyTime();
        SetVelocity();   
    }
    protected virtual void OnTriggerEnter2D(Collider2D col){
        //reference enemy script and make them take damage
        if (col.CompareTag("Enemy")){
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.takeDamage(damage);
            pierce -= 1;
            if(pierce <= 0){
                Destroy(gameObject);
            }
        }
    }

    void SetVelocity(){

        rb.linearVelocity = transform.right * projSpeed;
    }

    void SetDestroyTime(){
        Destroy(gameObject, destroyTime);

    }
}
