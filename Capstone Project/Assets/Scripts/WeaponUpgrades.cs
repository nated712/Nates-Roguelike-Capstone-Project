using UnityEngine;
using TMPro;
using System.Collections; // Needed for IEnumerator

public class WeaponUpgrades : MonoBehaviour
{
    [SerializeField] private float shootDelayReduction = 0.03f;
    [SerializeField] private float manaCostReduction = 1f;
    [SerializeField] private int specialShotReduction = 1;
    [SerializeField] private int specialShotReduction2 = 1;
    [SerializeField] private int minShotsToSpecial = 3;

    [SerializeField] private TextMeshProUGUI upgradeText; // Reference for the upgrade text

    private enum UpgradeType
    {
        ShootDelay,
        ManaCost,
        ShotsToSpecial,
        ShotsToSpecial2
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BasicSpellManager spellManager = other.GetComponentInChildren<BasicSpellManager>();
            if (spellManager != null)
            {
                // Find the UpgradeText in the scene if it's not already set
                if (upgradeText == null)
                {
                    upgradeText = GameObject.Find("UpgradeText").GetComponent<TextMeshProUGUI>();
                }

                ApplyRandomUpgrade(spellManager);
                Destroy(gameObject); // Remove the pickup after use
            }
        }
    }

    private void ApplyRandomUpgrade(BasicSpellManager spellManager)
    {
        UpgradeType upgrade = (UpgradeType)Random.Range(0, System.Enum.GetValues(typeof(UpgradeType)).Length);

        string message = "";

        switch (upgrade)
        {
            case UpgradeType.ShootDelay:
                spellManager.ReduceShootDelay(shootDelayReduction);
                message = "Upgraded Shooting Speed!";
                break;

            case UpgradeType.ManaCost:
                spellManager.ReduceManaCost(manaCostReduction);
                message = "Upgraded Max Mana!";
                break;

            case UpgradeType.ShotsToSpecial:
                spellManager.shotsToSpecial = Mathf.Max(minShotsToSpecial, spellManager.shotsToSpecial - specialShotReduction);
                spellManager.ResetShotCountIfTooHigh();
                message = "Upgraded Basic Attack Charge!";
                break;

            case UpgradeType.ShotsToSpecial2:
                spellManager.shotsToSpecial2 = Mathf.Max(minShotsToSpecial, spellManager.shotsToSpecial2 - specialShotReduction2);
                spellManager.ResetShotCountIfTooHigh();
                message = "Upgraded Special Attack Charge!";
                break;
        }

        // Show the upgrade text
        if (upgradeText != null)
        {
            upgradeText.text = message;
            upgradeText.gameObject.SetActive(true); // Make sure it's visible

            // Start coroutine to hide the text after a delay
            StartCoroutine(HideTextAfterDelay(2f));
        }
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (upgradeText != null)
        {
            upgradeText.gameObject.SetActive(false); // Hide the text after the delay
        }
    }
}
