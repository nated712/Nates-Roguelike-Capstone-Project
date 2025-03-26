using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicSpellManager : MonoBehaviour
{ 
  

    [SerializeField] private GameObject staff;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject specialProj;
    [SerializeField] private Transform spawnPoint;
    Vector2 worldPosition;
    Vector2 direction;
    private int shotCount;
    [SerializeField] int shotsToSpecial;

    private GameObject projInst;

    // Add a delay time for shooting
    [SerializeField] private float shootDelay = .2f;  // Delay time in seconds
    private float lastShotTime = 0f;  // Track the last time a shot was fired


    void Start() {
        shotCount = 1;
    }
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

        if((Input.GetMouseButton(1) && Time.time >= lastShotTime + shootDelay) || (Input.GetMouseButton(0) && Time.time >= lastShotTime + shootDelay) ){
            Vector3 spawnPos = spawnPoint.position;
            spawnPos.z = -1;
            if(shotCount < shotsToSpecial){
                //regular shot
                //spawn bullet
                projInst = Instantiate(projectile, spawnPos, staff.transform.rotation);
                lastShotTime = Time.time;
                shotCount++;
            } 
            //if special shot
            else if(shotCount == shotsToSpecial){
                shotCount = 1;
                projInst = Instantiate(specialProj, spawnPos, staff.transform.rotation);
                lastShotTime = Time.time;
            }
        }



    }


}
