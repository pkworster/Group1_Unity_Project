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
        health_Bar.SetHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            //this is just to test functionality... will be on event.
        {
            TakeDamage(10);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        health_Bar.SetHealth(currentHealth);
    }
}
// player health script - Peter Worster
