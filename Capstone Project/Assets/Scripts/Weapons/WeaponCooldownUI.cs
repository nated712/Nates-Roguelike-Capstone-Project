using UnityEngine;
using UnityEngine.UI;

public class WeaponCooldownUI : MonoBehaviour
{
    [Header("Shield")]
    public WeaponController shieldController;
    public Slider shieldCooldownSlider;

    [Header("Spear")]
    public WeaponController spearController;
    public Slider spearCooldownSlider;

    void Start()
    {
        if (shieldCooldownSlider != null)
            shieldCooldownSlider.maxValue = 1f;

        if (spearCooldownSlider != null)
            spearCooldownSlider.maxValue = 1f;
    }

    void Update()
    {
        if (shieldCooldownSlider != null && shieldController != null)
        {
            float shieldValue = shieldController.MaxCooldown > 0
                ? shieldController.currentCooldown / shieldController.MaxCooldown
                : 0f;

            shieldCooldownSlider.value = shieldValue;
        }

        if (spearCooldownSlider != null && spearController != null)
        {
            float spearValue = spearController.MaxCooldown > 0
                ? spearController.currentCooldown / spearController.MaxCooldown
                : 0f;

            spearCooldownSlider.value = spearValue;
        }
    }
}
