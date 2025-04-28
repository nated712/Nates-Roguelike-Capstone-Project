using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BossMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private Transform player;
    private bool pause;
    private Animator animator;
    public float attackRange = 25f;
    public float attackCooldown = 5f;
    private bool isAttacking = false;
    private float lastAttackTime;
    private Vector3 scale;
    private Vector2 chargeDirection; //so direction is locked when charging
    private bool isCharging = false; //checks if charging
    public float chargeSpeedMultiplier = 3f; //boss moves faster while charging
    public float chargeDuration = 2f;
    private float chargeStartTime;

    void Start()
    {
        scale = transform.localScale;
        player = FindFirstObjectByType<PManager>().transform;
        pause = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!pause)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (!isCharging)
            {
                if (distanceToPlayer < attackRange && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
                {
                    // Start charging
                    isCharging = true;
                    chargeStartTime = Time.time;
                    chargeDirection = (player.position - transform.position).normalized; // lock the direction
                    lastAttackTime = Time.time;
                }
                else if (!isAttacking)
                {
                    Move();
                }
            }
            else
            {   
                Charge();
            }
        }

        if (!isCharging) // Only face the player when NOT charging
        {
            FacePlayer();
        }
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

    void Charge()
    {
        float step = enemyData.MoveSpeed * chargeSpeedMultiplier * Time.deltaTime;

        Vector2 previousPosition = transform.position;
        transform.position += (Vector3)(chargeDirection * step);

        float speed = (transform.position - (Vector3)previousPosition).magnitude / Time.deltaTime;
        animator.SetFloat("Speed", speed);

        if (Time.time - chargeStartTime >= chargeDuration)
        {
            isCharging = false;
            isAttacking = false;
        }
    }

    void FacePlayer()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            transform.localScale = new Vector3(
                -Mathf.Sign(direction.x) * Mathf.Abs(scale.x),
                scale.y,
                scale.z
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManaManager hm = other.GetComponent<HealthManaManager>();
            if (hm != null)
            {
                hm.takeDamage(40);
            }

            StartCoroutine(stopMoving());
        }
    }

    IEnumerator stopMoving()
    {
        animator.SetFloat("Speed", 0f);
        pause = true;
        yield return new WaitForSeconds(1.3f);
        pause = false;
    }
}