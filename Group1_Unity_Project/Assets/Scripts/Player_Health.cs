using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public Health_Bar health_Bar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        health_Bar.maxValue = maxHealth;
    }

    // Update is called once per frame
    public void UpdateHealth(float mod)
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            health_Bar.value = currentHealth;
            PlayerDied();
        }
    }
    private void ()
        {
        LevelManager.instance.GameOver();
        }
        
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        health_Bar.SetHealth(currentHealth);
    }
}

// player health script - Peter Worster
// Test Parts out - death screen tie ins - Peter Worster 11/22
