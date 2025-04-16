using UnityEngine;

public class HPPickUp : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManaManager hm = other.GetComponent<HealthManaManager>();
            if (hm != null)
            {
                hm.takeDamage(-20);
            }
            Destroy(gameObject);
        }
        
    }



}
