using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float horizontalVelocity;
    public float health;

    public bool isAwake = false;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAwake) {
            body.velocity = Vector2.right * horizontalVelocity;
        }
        if(health <= 0) {
            Destroy(gameObject);
        }
    }

    public void Damage(int dmg) {
        Debug.Log("In damage function");
        health = health - dmg;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Triggering");
        if(other.gameObject.GetComponent<PlayerController>() != null) {
            Debug.Log("Player isn't null");
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.Damage(4);
        }
    }
}
