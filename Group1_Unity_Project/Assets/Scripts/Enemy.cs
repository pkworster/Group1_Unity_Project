using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float horizontalVelocity;
    public float health;
    public int damagePower;
    public float moveInterval = 4.0f; //Time enemy spends moving forwards
    public float stopInterval = 1.0f; //Time enemy spends stopping
    public float jumpInterval = 2.5f; // Time enemy waits between jumps
    private float stopTimeEnd; //Timers for each
    private float moveTimeEnd;
    private float jumpTimeEnd;

    public int fallBoundary = -40;

    public bool isReady = false;
    private bool isMoving;
    private bool isJumping;
    public float jumpPower;
    private Rigidbody2D body;
    private CircleCollider2D enemyCollider;
    public LayerMask jumpLayer;
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        enemyCollider = gameObject.GetComponent<CircleCollider2D>();
        Stop(stopInterval);
        jumpTimeEnd = Time.time + jumpInterval;
        
    }

    // Update is called once per frame

    private void FixedUpdate() 
    {
        if(Time.time > jumpTimeEnd && isMoving){
            Jump();
            jumpTimeEnd = Time.time + jumpInterval;
        }
        //First checks to see if it's "on" or ready
        if(isReady){
            if(isMoving) {
                //If we're set to move then move horizontally
                body.velocity = new Vector2(horizontalVelocity, body.velocity.y);

                //If the timer has passed our set movement time then stop
                if(Time.time > moveTimeEnd && isGrounded()){
                    Stop(stopInterval);
                }
            } else {
                //If we're not set to move then stop moving
                body.velocity = new Vector2(0f, body.velocity.y);

                //Check if we've passed our set stopping time and if we have, move
                if(Time.time > stopTimeEnd) {
                    //Set a new random direction.
                    if (Random.Range(0, 2) == 1){
                        horizontalVelocity = horizontalVelocity * -1;
                        transform.Rotate(0f, 180f, 0f);
                    }
                    Move(moveInterval);
                }
            }
        }
        
    }
    void Update()
    {
        //If the enemy goes below the fall boundary, it dies
        if (transform.position.y <= fallBoundary){
            Kill();
        }
        
    }

    public void Damage(int dmg) {
        if(isReady){
            health = health - dmg;

            //Kill our enemy if it has no health
            if(health <= 0) {
                Kill();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //If we see that the object we collided into has a PlayerController script we call its Damage function
        if(other.collider.gameObject.GetComponent<PlayerController>() != null) {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.Damage(damagePower); //This can be set to whatever. 
        }
    }


    private void Move(float moveTime)
    {
        isMoving = true;
        moveTimeEnd = Time.time + moveTime;
    }

    private void Stop(float stopTime) 
    {
        isMoving = false;
        stopTimeEnd = Time.time + stopTime;
    }

    private void Jump()
    {
        body.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
    }

    private bool isGrounded() 
    {
        return Physics2D.BoxCast(enemyCollider.bounds.center, enemyCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpLayer);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
