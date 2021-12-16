using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    TextMeshProUGUI killCounter_TMP;
    public int killCount;
    [SerializeField] GameObject deathScreen;

    // this is just to toggle Death screen on and off - Peter Worster
    public void ToggleDeathScreen()
    {
        deathScreen.SetActive(!deathScreen.activeSelf);
    }

    
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateKillCounterUI()
    {
        killCounter_TMP.text = killCount.ToString();
    }
}


