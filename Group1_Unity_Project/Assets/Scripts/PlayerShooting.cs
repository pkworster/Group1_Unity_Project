using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    public GameObject projectile;
    public float cooldown = 1.5f;
    private bool justFired;
    public float nextFire;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() 
    {
        if (justFired) {
            Instantiate(projectile, transform.position, transform.rotation);
            justFired = false;
        }    
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left ctrl") && Time.time > nextFire)
        {
            justFired = true;
            nextFire = Time.time + cooldown;
        }
    }
}
