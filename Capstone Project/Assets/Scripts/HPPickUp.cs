using UnityEngine;

public class HPPickUp : MonoBehaviour
{
    public AudioClip levelUpSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManaManager hm = other.GetComponent<HealthManaManager>();
            if (hm != null && hm.currentHP < hm.maxHealth){
                AudioSource.PlayClipAtPoint(levelUpSound, other.transform.position);
                    hm.takeDamage(-20);
                Destroy(gameObject);
            }
        }
        
    }



}
