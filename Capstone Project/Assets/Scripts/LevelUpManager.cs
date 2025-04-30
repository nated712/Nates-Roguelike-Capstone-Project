using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPManager : MonoBehaviour
{
    [SerializeField] private Slider upgradeProgressSlider;
    [SerializeField] private GameObject weaponUpgradeDrop;
    private GameObject player;
    public TextMeshProUGUI levelText;
    private int totalKillCount = 0;
    private int lastUpgradeKillCount = 0;
    private int requiredKills = 5;
    private int increaseKillsForNextUpgrade = 7;
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

    public void RegisterEnemyKill()
    {
        totalKillCount++;
        UpdateProgressBar();

        if (totalKillCount - lastUpgradeKillCount >= requiredKills)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentLevel++;
        levelEffect.Play();
        levelText.text = currentLevel.ToString();
        lastUpgradeKillCount = totalKillCount;
        requiredKills += increaseKillsForNextUpgrade;

        if (upgradeProgressSlider != null)
        {
            upgradeProgressSlider.maxValue = requiredKills;
            upgradeProgressSlider.value = 0; // Start fresh
        }
    }

    private void UpdateProgressBar()
    {
        if (upgradeProgressSlider != null)
        {
            upgradeProgressSlider.value = totalKillCount - lastUpgradeKillCount;
        }
    }
}
