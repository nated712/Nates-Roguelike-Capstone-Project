using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private Transform player;
    private bool pause;

    void Start()
    {
        player = FindFirstObjectByType<PManager>().transform;
        pause = false;
    }

    void Update()
    {
        Move();
        FacePlayer();
    }

    void Move()
    {
        if (!pause && player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyData.MoveSpeed * Time.deltaTime);
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
        pause = true;
        yield return new WaitForSeconds(2);
        pause = false;
    }
}
