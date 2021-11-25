using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXS : MonoBehaviour
{
    public AudioSource Fire;
    public AudioSource EnemyDeath;
    public AudioSource PlayerHit;
    public AudioSource EnemyHit;
    public AudioSource Jump;

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
        if (Input.GetKeyDown("space"))
        {
            if (!Jump.isPlaying)
            {
                Jump.Play();
            }
        }
        if (Input.GetKeyUp("space"))
        {
            Jump.Stop();
        }
    }
}
