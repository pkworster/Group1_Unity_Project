using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    public float speed = 20.0f;
    private Rigidbody2D playerBody;
    private BoxCollider2D playerCollider;


    public float jumpForce = 30.0f;
    public LayerMask jumpLayer;
    private bool justJumped;
    private bool isJumping;

    public int fallBoundary = -40;

    private float horizontalVelocity;
    private float verticalVelocity;

    public int health = 10;
    private float damageCooldown = 1.0f; // Time before player can take damage again in seconds

    private float nextDamageTime;

    int direction = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        //Grab our player's Rigidbody and Collider
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<BoxCollider2D>();

    }
    
    void FixedUpdate() 
    {
        //If we just jumped, add an initial force.
        if(justJumped){
            playerBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            justJumped = false;
        }

        //Change player horizontal velocity based on the pressed key.
        horizontalVelocity = speed * horizontalInput;
        playerBody.velocity = new Vector2(horizontalVelocity, playerBody.velocity.y);

        //Flip the character around the x axis if they change direction.
        if (horizontalVelocity * direction < 0f) {
            direction *= -1;
            transform.Rotate(0f, 180f, 0f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        //If the player goes below the fall boundary, it dies
        if (transform.position.y <= fallBoundary){
            KillPlayer();
        }

        horizontalInput = Input.GetAxis("Horizontal");

        //Check if the player is pressing down and is on ground.
        if(Input.GetKeyDown("space") && isGrounded()) 
        {
            justJumped = true;
            
        }

        //Reset the player's position by pressing 'r'
        if (Input.GetKeyDown("r"))
        {
            transform.position = new Vector2(0,0);
        }
    }

    //Returns true if the player is on top of something
    private bool isGrounded() 
    {
        return Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpLayer);
    }

    public void Damage(int dmg) 
    {
        
        if(Time.time > nextDamageTime) {  
            health = health - dmg;
            if (health <=0)
            {
                KillPlayer();
            }
        } else {
            nextDamageTime = Time.time + damageCooldown;
        }
    }

    public void KillPlayer()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
  
    }

}
