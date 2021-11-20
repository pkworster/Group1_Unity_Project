using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    public GameObject projectile;
    private bool justFired;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() 
    {
        if (justFired)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            justFired = false;
        }    
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left ctrl")){
            justFired = true;
        }
    }
}
