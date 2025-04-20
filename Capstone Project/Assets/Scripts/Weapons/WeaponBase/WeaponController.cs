using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class WeaponController : MonoBehaviour
{
    //Base for all weapons
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    public float currentCooldown;
    public float MaxCooldown;
    protected PManager pm;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pm =FindFirstObjectByType<PManager>();
        //Weapon waits before firing
        currentCooldown = weaponData.CooldownDuration;
        MaxCooldown = weaponData.CooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();  
        }
    }

    protected virtual void Attack(){
        currentCooldown = weaponData.CooldownDuration;

    }
}
