using System.Collections;
using UnityEngine;

public class RangedEnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private Transform player;
    private bool pause;
    private Animator animator;

    public GameObject projectilePrefab;
    public Transform firePoint;  // where the projectile spawns from
    public float attackRange = 5f;
    public float attackCooldown = 2f;
    private bool isAttacking = false;
    private float lastAttackTime;

    void Start()
    {
        player = FindFirstObjectByType<PManager>().transform;
        pause = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!pause)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < attackRange && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
            {
                Debug.Log("Enemy is attacking!");
                animator.SetTrigger("Attack");
                isAttacking = true;
            } else if(!isAttacking)
            {
                Move();
            }
    }

    FacePlayer();
    }

    void Move()
    {
        if (!pause && player != null)
        {
            float step = enemyData.MoveSpeed * Time.deltaTime;
            Vector2 previousPosition = transform.position;

            transform.position = Vector2.MoveTowards(transform.position, player.position, step);

            float speed = ((Vector2)transform.position - previousPosition).magnitude / Time.deltaTime;
            animator.SetFloat("Speed", speed);  // ← Set parameter here
        }   else{
            animator.SetFloat("Speed", 0f); // If paused, set speed to 0
        }
    }

    void FacePlayer()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            transform.localScale = new Vector3(direction.x >= 0 ? -1 : 1, 1, 1);
        }
    }

    public void ShootProjectile()
    {
        if (firePoint != null && projectilePrefab != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Vector2 direction = (player.position - firePoint.position).normalized;
            proj.GetComponent<Rigidbody2D>().linearVelocity = direction * 20f; // or whatever speed
            lastAttackTime = Time.time;
            isAttacking = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManaManager hm = other.GetComponent<HealthManaManager>();
            if (hm != null)
            {
                hm.takeDamage(20);
            }

            StartCoroutine(stopMoving());
        }
    }

    IEnumerator stopMoving()
    {
        animator.SetFloat("Speed", 0f);
        pause = true;
        yield return new WaitForSeconds(1f);
        pause = false;
    }
}

