using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotateBasicSpell : MonoBehaviour
{ 
  

    [SerializeField] private GameObject staff;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnPoint;
    Vector2 worldPosition;
    Vector2 direction;

    private GameObject projInst;

    // Add a delay time for shooting
    [SerializeField] private float shootDelay = .2f;  // Delay time in seconds
    private float lastShotTime = 0f;  // Track the last time a shot was fired

    void Update() {
        Rotation();
        Shoot();
    }
   
    void Rotation(){
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (worldPosition - (Vector2)staff.transform.position).normalized;
        staff.transform.right = direction;
    }

    void Shoot(){

        if(Input.GetMouseButton(0) && Time.time >= lastShotTime + shootDelay){
            //spawn bullet
            projInst = Instantiate(projectile, spawnPoint.position, staff.transform.rotation);

            lastShotTime = Time.time;
        }

        if(Input.GetMouseButton(1) && Time.time >= lastShotTime + shootDelay){
            projInst = Instantiate(projectile, spawnPoint.position, staff.transform.rotation);

            lastShotTime = Time.time;
        }



    }


}
