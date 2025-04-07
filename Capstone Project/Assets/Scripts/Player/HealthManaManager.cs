using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class HealthManaManager : MonoBehaviour
{
    public float maxMana = 100f;
    public float currentMana;
    public float manaRegenRate = 20f;
    public float maxHealth = 100f;
    public float currentHP;
    public TMP_Text healthText;
    public TMP_Text manaText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHealth;
        currentMana = maxMana;
        healthText.text = currentHP.ToString();
        manaText.text = currentMana.ToString();
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
        healthText.text = currentHP.ToString();
        if(currentHP <= 0){    
            Die();      
        }
    }

    public void RegenMana(){
        if (currentMana < maxMana){
            currentMana += manaRegenRate * Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);
            manaText.text = currentMana.ToString();
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
        manaText.text = currentMana.ToString();
    }

    public void giveMana(float amount){
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        manaText.text = currentMana.ToString();
    }



    public void Die(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
