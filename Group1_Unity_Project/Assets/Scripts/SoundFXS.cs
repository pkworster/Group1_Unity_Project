using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXS : MonoBehaviour
{
    public AudioSource Fire;
    public AudioSource EnemyDeath;
    public AudioSource PlayerHit;
    public AudioSource EnemyHit;

    public void PlayFire ()
    {
        Fire.Play();
    }
    void Update()
    {
        if (Input.GetKeyDown("left ctrl"))
        {
            if (!Fire.isPlaying)
            {
                Fire.Play();
            }
        }
        if (Input.GetKeyUp("left ctrl"))
        {
            Fire.Stop();
        }

    }
}
