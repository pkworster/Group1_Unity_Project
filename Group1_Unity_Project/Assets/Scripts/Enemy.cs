using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float horizontalVelocity;
    //public float verticalVelocity;
    public float health;
    public float movePatternTime = 4.0f;
    public float stopPatternTime = 1.0f;
    public float jumpPatternTime = 2.5f;
    private float stopTimeEnd;
    private float moveTimeEnd;
    private float jumpTimeEnd;

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
        Stop(stopPatternTime);
        jumpTimeEnd = Time.time + jumpPatternTime;
        
    }

    // Update is called once per frame

    private void FixedUpdate() 
    {
        if(Time.time > jumpTimeEnd && isMoving){
            Jump();
            jumpTimeEnd = Time.time + jumpPatternTime;
        }
        
    }
    void Update()
    {
        //First checks to see if it's "on" or ready
        if(isReady){
            if(isMoving) {
                //If we're set to move then move horizontally
                body.velocity = new Vector2(horizontalVelocity, body.velocity.y);

                //If the timer has passed our set movement time then stop
                if(Time.time > moveTimeEnd && isGrounded()){
                    Stop(stopPatternTime);
                }
            } else {
                //If we're not set to move then stop moving
                body.velocity = new Vector2(0f, body.velocity.y);

                //Check if we've passed our set stopping time and if we have, move
                if(Time.time > stopTimeEnd) {
                    //Set a new random direction.
                    int nextDirection = Random.Range(0,2);
                    if(nextDirection == 1){
                        horizontalVelocity = horizontalVelocity * -1;
                        transform.Rotate(0f, 180f, 0f);
                    }
                    Move(movePatternTime);
                }
            }
        }

        //Kill our enemy if it has no health
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //If we see that the object we collided into has a PlayerController script we call its Damage function
        if(other.collider.gameObject.GetComponent<PlayerController>() != null) {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.Damage(4); //This can be set to whatever.
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
}
