using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    [SerializeField] private Slider upgradeProgressSlider;
    [SerializeField] private GameObject weaponUpgradeDrop;
    private GameObject player;

    private int totalKillCount = 0;
    private int lastUpgradeKillCount = 0;
    private int requiredKills = 5;
    private int increaseKillsForNextUpgrade = 7;

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

    private void LevelUp()
    {
        Instantiate(weaponUpgradeDrop, player.transform.position, Quaternion.identity);

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
