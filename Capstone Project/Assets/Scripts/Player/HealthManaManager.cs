using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class HealthManaManager : MonoBehaviour
{
    public float maxMana = 100f;
    public float currentMana;
    public float manaRegenRate = 7f;
    public float maxHealth = 100f;
    public float currentHP;
    public Slider HPslider;
    public Slider MPslider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHealth;
        currentMana = maxMana;
        HPslider.maxValue = maxHealth;
        HPslider.value = currentHP;
        MPslider.maxValue = maxMana;
        MPslider.value = currentMana;
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
        HPslider.value = currentHP;
        if(currentHP <= 0){    
            Die();      
        }
    }

    public void RegenMana(){
        if (currentMana < maxMana){
            currentMana += manaRegenRate * Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);
            MPslider.value = currentMana;
        }
    }

    public void spendMana(float amount){
        currentMana -= amount;
        if (currentMana < 0)
        {
            currentMana = 0;
        }
        MPslider.value = currentMana;
    }

    public void giveMana(float amount){
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        MPslider.value = currentMana;
    }



    public void Die(){
        //load death scene
        SceneManager.LoadScene(3);
    }
    
}
