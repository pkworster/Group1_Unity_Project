using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    
    public Rigidbody2D bullet;
    private Collider2D bulletCollider;
    public float speed = -10.0f;
    private float startTime;
    private float deathTime;
    public float maxTime = 8.0f; // Max time of bullet before it destroys itself
    public float rotationSpeed = -1200.0f; 


    // Start is called before the first frame update
    void Start()
    {
        bulletCollider = gameObject.GetComponent<Collider2D>();
        deathTime = Time.time + maxTime;
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
        bullet.transform.GetChild(0).Rotate(transform.forward * rotationSpeed * Time.fixedDeltaTime);    
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.GetComponent<Player>() != null) {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Damage(20);
        }
        Destroy(gameObject);
    }
}
