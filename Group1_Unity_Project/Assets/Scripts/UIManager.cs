using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;

    public void ToggleDeathScreen()
    {
        deathScreen.SetActive(!deathScreen.activeSelf);
    }
}
// this is just to toggle Death screen on and off - Peter Worster