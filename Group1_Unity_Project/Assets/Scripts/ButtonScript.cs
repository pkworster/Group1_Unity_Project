using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void ChangeToCredits (int sceneToChangeTo)
    {
        Application.LoadLevel(sceneToChangeTo);
    }
}

// quick button for credits page - Peter Worster 12/16