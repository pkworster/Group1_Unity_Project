using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Func : MonoBehaviour
{   
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void QuitGame ()
    {
        Application.Quit();
        Debug.Log("Goodbye!");
    }
}
// Setup and Functionality for Main Menu - Peter Worster