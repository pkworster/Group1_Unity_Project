using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float horizontalVelocity;
    public float health;
    public float moveTime = 2.0f;
    public float stopTime = 2.0f;
    private float stopTimeEnd;
    private float moveTimeEnd;

    public bool isReady = false;
    [SerializeField]
    private bool isMoving;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        stopTimeEnd = Time.time + stopTime;
    }

    // Update is called once per frame
    void Update()
    {
        //First checks to see if it's "on" or ready
        if(isReady){
            if(isMoving) {
                //If we're set to move then move horizontally
                body.velocity = Vector2.right * horizontalVelocity;

                //If the timer has passed our set movement time then stop and set a new random direction.
                if(Time.time > moveTimeEnd){
                    Stop();
                    int nextDirection = Random.Range(0,2);
                    if(nextDirection == 1){
                        horizontalVelocity = horizontalVelocity * -1;
                    }
                }
            } else {
                //If we're not set to move then stop moving
                body.velocity = Vector2.zero;

                //Check if we've passed our set stopping time and if we have, move
                if(Time.time > stopTimeEnd) {
                    Move();
                }
            }
        }

        if(health <= 0) {
            Destroy(gameObject);
        }
    }

    public void Damage(int dmg) {
        if(isReady){
            Debug.Log("In damage function");
            health = health - dmg;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //If we see that the object we collided into has a PlayerController script we call its Damage function
        if(other.gameObject.GetComponent<PlayerController>() != null) {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.Damage(4); //This can be set to whatever.
        }
    }


    private void Move()
    {
        isMoving = true;
        moveTimeEnd = Time.time + moveTime;
    }

    private void Stop() 
    {
        isMoving = false;
        stopTimeEnd = Time.time + stopTime;
    }
}
