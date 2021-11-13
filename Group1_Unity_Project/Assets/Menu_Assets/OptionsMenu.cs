using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("master_volume", volume);
        Debug.Log(volume);
    }
}
//Setup and Funtionality for options menu - Peter Worster