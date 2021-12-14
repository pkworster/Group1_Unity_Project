using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArchived : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TakeDamage(20);
        }
        // Added death ... works with tab test - 12/6 Peter Worster
        if (currentHealth <= 0)
        {
            PlayerDied();
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
    private void PlayerDied()
    {
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }
    public void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == ("Enemy"))
        {
            TakeDamage(20);
        }
    }
}



// player health script - Peter Worster
// Test Parts out - death screen tie ins - Peter Worster 11/21
// wiped out... going to try something different 11/22
// 2nd or 3rd attempt 11/28
// 12/5 put test things back in... attempting a different version
