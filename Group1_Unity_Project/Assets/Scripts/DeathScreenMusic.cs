using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenMusic : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public AudioSource DeathMusic;

    bool PlayedDeathMusic = false;
  

// Start is called before the first frame update
void Start()
    {
        if (BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Stop();
        }
        if (!DeathMusic.isPlaying && PlayedDeathMusic == false)
        {
            DeathMusic.Play();
            PlayedDeathMusic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
// this hopefully stops main scene music on death. - Peter
// got it to work!!! 12/1 - Peter Worster