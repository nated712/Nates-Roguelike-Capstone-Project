using UnityEngine;

public class XPPickup : MonoBehaviour
{
    [SerializeField] private int xpAmount = 15;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            XPManager xpManager = FindFirstObjectByType<XPManager>();
            if (xpManager != null)
            {
                xpManager.GainXP(xpAmount);
            }
            Destroy(gameObject);
        }
    }
}
