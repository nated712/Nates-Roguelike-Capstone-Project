using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class XPManager : MonoBehaviour
{
    [SerializeField] private Slider upgradeProgressSlider;
    [SerializeField] private GameObject weaponUpgradeDrop;
    [SerializeField] public GameObject HPDrop;
    private GameObject player;
    public TextMeshProUGUI levelText;
    private int totalKillCount = 0;
    private int lastUpgradeKillCount = 0;
    public int requiredKills = 5;
    private int increaseKillsForNextUpgrade = 3;
    private int currentLevel = 1;
    public ParticleSystem levelEffect;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        if (upgradeProgressSlider == null)
        {
            upgradeProgressSlider = GameObject.Find("XPbar")?.GetComponent<Slider>();
        }

        if (upgradeProgressSlider != null)
        {
            upgradeProgressSlider.maxValue = requiredKills;
            upgradeProgressSlider.value = 0;
        }
        levelText.text = currentLevel.ToString();
    }

    public void RegisterEnemyKill(int value)
    {
        totalKillCount += value;

        // Loop to allow for multiple level-ups at once if xp is a lot
        while (totalKillCount - lastUpgradeKillCount >= requiredKills)
        {
            LevelUp();
        }

        UpdateProgressBar();
    }


    public void LevelUp()
    {
        currentLevel++;
        levelEffect.Play();
        levelText.text = currentLevel.ToString();

        lastUpgradeKillCount += requiredKills;
        requiredKills += increaseKillsForNextUpgrade;

        if (upgradeProgressSlider != null)
        {
            upgradeProgressSlider.maxValue = requiredKills;
    
        }
        // Spawn HP pickup every 5 levels
        if (currentLevel % 5 == 0 && HPDrop != null && player != null)
        {
            Instantiate(HPDrop, player.transform.position, Quaternion.identity);
        }
        TriggerUpgrade();
    }

    private void UpdateProgressBar()
    {
        if (upgradeProgressSlider != null)
        {
            upgradeProgressSlider.value = totalKillCount - lastUpgradeKillCount;
        }
    }

    public void GainXP(int amount)
    {
        totalKillCount += amount;

        while (totalKillCount - lastUpgradeKillCount >= requiredKills)
        {
            LevelUp();
        }

        UpdateProgressBar();
    }

    private void TriggerUpgrade()
{
    GameObject player = GameObject.FindWithTag("Player");
    if (player == null) return;

    BasicSpellManager spellManager = player.GetComponentInChildren<BasicSpellManager>();
    if (spellManager == null) return;

    WeaponUpgrades upgradeHandler = GetComponent<WeaponUpgrades>();
    if (upgradeHandler != null)
    {
        upgradeHandler.ApplyRandomUpgrade(spellManager, player);
    }
}

}
