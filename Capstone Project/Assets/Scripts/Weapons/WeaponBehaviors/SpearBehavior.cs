using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpearBehavior : ProjectileWeaponBehavior
{
    protected override void Start()
    {
        base.Start();
       
    }

    // Update is called once per frame
    void Update()
    {
        //set the movement of the spear
        transform.position += direction * weaponData.Speed * Time.deltaTime;
    }
}
