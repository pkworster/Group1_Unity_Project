using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 10.0f;
    public Rigidbody2D bullet;
    private float startTime;
    private float deathTimer;
    public float maxTime = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        deathTimer = startTime;
        bullet.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        deathTimer = deathTimer + Time.deltaTime;
        if (deathTimer - startTime > maxTime)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate() 
    {
        bullet.transform.Rotate(Vector3.forward * -600 * Time.deltaTime);    
    }

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
