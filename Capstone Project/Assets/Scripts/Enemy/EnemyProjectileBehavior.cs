using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;  // Speed of the projectile
    public int damage = 20;  // Damage dealt to the player
    public float lifetime = 3f;  // Time after which the projectile will be destroyed if it doesn't hit anything

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);  // Destroy the projectile after a set time
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("projectile collision");
        if (other.CompareTag("Player"))
        {
            HealthManaManager hm = other.GetComponent<HealthManaManager>();
            if (hm != null)
            {
                hm.takeDamage(damage);
            }
            Destroy(gameObject);

        }
    }

}