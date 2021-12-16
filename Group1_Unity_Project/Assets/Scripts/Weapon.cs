using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float cooldown = 1.5f;
    private bool justFired;
    private float nextFire;
    public GameObject bullet;

    //public int maxBullets; <- Currently not implemented
    public Transform shotPoint;


    void Awake() 
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate() 
    {
        if (justFired) {
            GameObject newBullet = Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);
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
