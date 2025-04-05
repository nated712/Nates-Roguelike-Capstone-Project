using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManaManager : MonoBehaviour
{
    public float maxMana = 100f;
    public float currentMana;
    public float manaRegenRate = 20f;
    public float maxHealth = 100f;
    public float currentHP;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHealth;
        currentMana = maxMana;
    }

    void Update()
    {
        RegenMana();
        // Check for "R" key press to reset the stage
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
    }
    
    public void takeDamage(float damageAmount){
        currentHP -= damageAmount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHealth);
        
        if(currentHP <= 0){    
            Die();      
        }
    }

    public void RegenMana(){
        if (currentMana < maxMana){
            currentMana += manaRegenRate * Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        }
    }

    public void spendMana(float amount){
        if (currentMana >= amount){
            currentMana -= amount;
            if (currentMana < 0)
            {
                currentMana = 0;
            }
        }
    }

    public void giveMana(float amount){
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
    }



    public void Die(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
