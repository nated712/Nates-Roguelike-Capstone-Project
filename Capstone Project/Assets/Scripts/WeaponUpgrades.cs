using UnityEngine;
using TMPro;
using System.Collections; // Needed for IEnumerator

public class WeaponUpgrades : MonoBehaviour
{
    [SerializeField] private float shootDelayReduction = 0.03f;
    [SerializeField] private float manaCostReduction = 1f;
    [SerializeField] private int specialShotReduction = 1;
    [SerializeField] private int specialShotReduction2 = 1;
    [SerializeField] private int minShotsToSpecial = 2;
    [SerializeField] private TextMeshProUGUI upgradeText;

    private enum UpgradeType
    {
        ShootDelay,
        ManaCost,
        ShotsToSpecial,
        ShotsToSpecial2,
        ShieldCooldown,
        SpearCooldown
    }

    // Track how many times each upgrade was picked
    private int shootDelayUpgradeCount = 0;
    private int manaCostUpgradeCount = 0;
    private int shotsToSpecialUpgradeCount = 0;
    private int shotsToSpecial2UpgradeCount = 0;
    private int shieldCooldownUpgradeCount = 0;
    private int spearCooldownUpgradeCount = 0;

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
                print("Fix this must level up from in here");
                ApplyRandomUpgrade(spellManager, other.gameObject);
                Destroy(gameObject); // Remove the pickup after use
            }
        }
    }

    private void ApplyRandomUpgrade(BasicSpellManager spellManager, GameObject player)
    {
        UpgradeType upgrade = (UpgradeType)Random.Range(0, System.Enum.GetValues(typeof(UpgradeType)).Length);
        string message = "";

        bool upgradeApplied = false;

        while (!upgradeApplied)
        {
            switch (upgrade)
            {
                case UpgradeType.ShootDelay:
                    shootDelayUpgradeCount++;
                    float scaledShootDelay = Mathf.Max(
                        shootDelayReduction * Mathf.Pow(0.5f, shootDelayUpgradeCount - 1),
                        shootDelayReduction * 0.05f // Minimum 5% effectiveness
                    );
                    spellManager.ReduceShootDelay(scaledShootDelay);
                    message = "Upgraded Shooting Speed!";
                    upgradeApplied = true;
                    break;

                case UpgradeType.ManaCost:
                    manaCostUpgradeCount++;
                    float scaledManaCost = Mathf.Max(
                        manaCostReduction * Mathf.Pow(0.75f, manaCostUpgradeCount - 1),
                        manaCostReduction * 0.1f
                    );
                    spellManager.ReduceManaCost(scaledManaCost);
                    message = "Upgraded Max Mana!";
                    upgradeApplied = true;
                    break;

                case UpgradeType.ShotsToSpecial:
                    if (spellManager.shotsToSpecial > minShotsToSpecial)
                    {
                        shotsToSpecialUpgradeCount++;
                        spellManager.shotsToSpecial = Mathf.Max(minShotsToSpecial, spellManager.shotsToSpecial - specialShotReduction);
                        spellManager.ResetShotCountIfTooHigh();
                        message = "Upgraded Basic Attack Charge!";
                        upgradeApplied = true;
                    }
                    else
                    {
                        // Reroll
                        upgrade = (UpgradeType)Random.Range(0, System.Enum.GetValues(typeof(UpgradeType)).Length);
                    }
                    break;

                case UpgradeType.ShotsToSpecial2:
                    if (spellManager.shotsToSpecial2 > minShotsToSpecial)
                    {
                        shotsToSpecial2UpgradeCount++;
                        spellManager.shotsToSpecial2 = Mathf.Max(minShotsToSpecial, spellManager.shotsToSpecial2 - specialShotReduction2);
                        spellManager.ResetShotCountIfTooHigh();
                        message = "Upgraded Special Attack Charge!";
                        upgradeApplied = true;
                    }
                    else
                    {
                        // Reroll
                        upgrade = (UpgradeType)Random.Range(0, System.Enum.GetValues(typeof(UpgradeType)).Length);
                    }
                    break;

                case UpgradeType.ShieldCooldown:
                    shieldCooldownUpgradeCount++;
                    float scaledShieldCooldown = Mathf.Max(
                        0.2f * Mathf.Pow(0.75f, shieldCooldownUpgradeCount - 1),
                        0.2f * 0.1f
                    );
                    ShieldController shieldController = player.GetComponentInChildren<ShieldController>();
                    if (shieldController != null)
                    {
                        shieldController.UpgradeCooldown(scaledShieldCooldown);
                        message = "Upgraded Shield Cooldown!";
                    }
                    else
                    {
                        message = "No Shield Controller Found!";
                    }
                    upgradeApplied = true;
                    break;

                case UpgradeType.SpearCooldown:
                    spearCooldownUpgradeCount++;
                    float scaledSpearCooldown = Mathf.Max(
                        0.2f * Mathf.Pow(0.75f, shieldCooldownUpgradeCount - 1),
                        0.2f * 0.1f
                    );
                    SpearController spearController = player.GetComponentInChildren<SpearController>();
                    if (spearController != null)
                    {
                        spearController.UpgradeCooldown(scaledSpearCooldown);
                        message = "Upgraded Knife Cooldown!";
                    }
                    else
                    {
                        message = "No Spear Controller Found!";
                    }
                    upgradeApplied = true;
                    break;
            }
        }

        // Show the upgrade text
        if (upgradeText != null)
        {
            upgradeText.text = message;
            upgradeText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterDelay(2f));
        }
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (upgradeText != null)
        {
            upgradeText.gameObject.SetActive(false);
        }
    }
}
