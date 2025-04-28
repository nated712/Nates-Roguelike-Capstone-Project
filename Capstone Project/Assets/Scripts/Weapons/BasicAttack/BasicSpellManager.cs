using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicSpellManager : MonoBehaviour
{ 
    [SerializeField] private GameObject staff;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject specialProj;
    [SerializeField] private GameObject tier3Proj;
    [SerializeField] private Transform spawnPoint;
    Vector2 worldPosition;
    Vector2 direction;
    private int shotCount;
    private int shotCount2;
    [SerializeField] public int shotsToSpecial;
    [SerializeField] public int shotsToSpecial2;
    public HealthManaManager hmm;
    public float spellManaCost = 7f;

    private GameObject projInst;

    // Add a delay time for shooting
    [SerializeField] private float shootDelay = .2f;  // Delay time in seconds
    private float lastShotTime = 0f;  // Track the last time a shot was fired


    void Start() {
        shotCount = 1;
        shotCount2 = 1;
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
    public void ReduceShootDelay(float amount)
    {
        shootDelay = Mathf.Max(0.05f, shootDelay - amount); // Clamp to avoid too fast
    }

    public void ReduceManaCost(float amount)
    {
        spellManaCost = Mathf.Max(1f, spellManaCost - amount); // Clamp to avoid zero cost
    }
    public void ResetShotCountIfTooHigh()
    {
        shotCount = Mathf.Min(shotCount, shotsToSpecial);
        shotCount2 = Mathf.Min(shotCount2, shotsToSpecial2);
    }

    void Shoot(){
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.z = -1;
        if(Input.GetMouseButton(0) && Time.time >= lastShotTime + shootDelay){
            if(shotCount < shotsToSpecial && hmm.currentMana > spellManaCost){
                //regular shot
                //spawn bullet
                projInst = Instantiate(projectile, spawnPos, staff.transform.rotation);
                lastShotTime = Time.time;
                shotCount++;
                hmm.spendMana(spellManaCost);
            } 
            //if special shot
            else if(shotCount == shotsToSpecial && hmm.currentMana > spellManaCost){
                shotCount = 1;
                projInst = Instantiate(specialProj, spawnPos, staff.transform.rotation);
                lastShotTime = Time.time;
                hmm.spendMana(spellManaCost);
            }       
        } else if (Input.GetMouseButton(1) && Time.time >= lastShotTime + shootDelay){
                if(shotCount2 < shotsToSpecial2 && hmm.currentMana > (spellManaCost * 2.5f)){
                    //special shot command
                    //spawn bullet
                    projInst = Instantiate(specialProj, spawnPos, staff.transform.rotation);
                    lastShotTime = Time.time;
                    shotCount2++;
                    hmm.spendMana(spellManaCost * 1.5f);
            }   else if(shotCount2 == shotsToSpecial2 && hmm.currentMana > (spellManaCost * 2.5f)){
                    shotCount2 = 1;
                    projInst = Instantiate(tier3Proj, spawnPos, staff.transform.rotation);
                    lastShotTime = Time.time;
                    hmm.spendMana(spellManaCost * 1.5f);
            }     
        }
    }
}
