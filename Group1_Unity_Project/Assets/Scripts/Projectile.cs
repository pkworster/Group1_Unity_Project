using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Rigidbody2D bullet;
    public float speed = 10.0f;
    private float startTime;
    private float deathTime;
    public float maxTime = 8.0f; // Max time of bullet before it destroys itself
    public float rotationSpeed = -1200.0f; 


    // Start is called before the first frame update
    void Start()
    {
        deathTime = Time.time + maxTime;
        bullet.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > deathTime)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate() 
    {
        //Rotate the bullet every frame cause why not
        bullet.transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);    
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.GetComponent<Enemy>() != null) {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Damage(1);
        }

        Destroy(gameObject);
    }
}
