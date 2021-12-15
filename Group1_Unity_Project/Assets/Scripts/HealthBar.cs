using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public Gradient gradient;
    public Image fill;

    public Player playerObject;

    private void Start() 
    {
        slider = gameObject.GetComponent<Slider>();
        SetMaxHealth(100);
    }
    private void Update() 
    {
        SetHealth(playerObject.health);
    }
    public void SetMaxHealth(int health)
    {   
        slider.maxValue = health;
        slider.value = health;

        
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        
    }
    
    //Healthbar functionality - Peter Worster
    // took out gradient refs - 12/14 - Peter Worster
   
}
