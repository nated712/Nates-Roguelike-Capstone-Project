using UnityEngine;

public class WeaponUpgrades : MonoBehaviour
{
    [SerializeField] private float shootDelayReduction = 0.05f;
    [SerializeField] private float manaCostReduction = 1f;
    [SerializeField] private int specialShotReduction = 1;

    private enum UpgradeType
    {
        ShootDelay,
        ManaCost,
        ShotsToSpecial,
        ShotsToSpecial2
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            BasicSpellManager spellManager = other.GetComponentInChildren<BasicSpellManager>();
            if (spellManager != null)
            {
                ApplyRandomUpgrade(spellManager);
                Destroy(gameObject); // Remove the pickup after use
            }
        }
    }

    private void ApplyRandomUpgrade(BasicSpellManager spellManager)
    {
        UpgradeType upgrade = (UpgradeType)Random.Range(0, System.Enum.GetValues(typeof(UpgradeType)).Length);

        switch (upgrade)
        {
            case UpgradeType.ShootDelay:
                spellManager.ReduceShootDelay(shootDelayReduction);
                Debug.Log("Upgrade: Reduced shoot delay!");
                break;
            case UpgradeType.ManaCost:
                spellManager.ReduceManaCost(manaCostReduction);
                Debug.Log("Upgrade: Reduced mana cost!");
                break;
            case UpgradeType.ShotsToSpecial:
                spellManager.shotsToSpecial = Mathf.Max(1, spellManager.shotsToSpecial - specialShotReduction);
                Debug.Log("Upgrade: Reduced shots needed for special spell!");
                break;
            case UpgradeType.ShotsToSpecial2:
                spellManager.shotsToSpecial2 = Mathf.Max(1, spellManager.shotsToSpecial2 - specialShotReduction);
                Debug.Log("Upgrade: Reduced shots needed for tier 3 spell!");
                break;
        }
    }
}
