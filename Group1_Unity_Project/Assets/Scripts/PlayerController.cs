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
    public Color invulnerableColor;
    public float damageRecoil = 10.0f;
    private float damageCooldown = 1.0f; // Time before player can take damage again in seconds

    private float nextDamageTime;
    private bool inputEnabled;
    private float inputTimer;

    int direction = 1;

    //attempting to animate sprite - Peter Worster
    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        inputEnabled = true;
        nextDamageTime = 0;
        //Grab our player's Rigidbody and Collider
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
    }
    
    void FixedUpdate() 
    {
        if(inputEnabled) {
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
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time > inputTimer && inputEnabled == false) {
            inputEnabled = true;
        }
        // Change back to default color if the player can be damaged
        if(Time.time > nextDamageTime) {
            foreach (Transform child in transform)
            {
                if(child.gameObject.GetComponent<SpriteRenderer>() != null){
                SpriteRenderer render =  child.gameObject.GetComponent<SpriteRenderer>();
                render.color = Color.white;
                }
            }
        }
        //If the player goes below the fall boundary, it dies
        if (transform.position.y <= fallBoundary){
            KillPlayer();
        }

        horizontalInput = Input.GetAxis("Horizontal");

        // prob should be in its own movement script...but hey I'm gonna try this first - Peter Worster
        // added Mathf so it animated when moving left - Peter
        animator.SetFloat("Speed", Mathf.Abs (horizontalInput));

        //Check if the player is pressing down and is on ground.
        if (Input.GetKeyDown("space") && isGrounded()) 
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
            
            //Disable the input
            inputEnabled = false;
            inputTimer = Time.time + 0.5f;

            //Change all the player's objects' transparencies
            foreach (Transform child in transform)
            {
                if(child.gameObject.GetComponent<SpriteRenderer>() != null){
                SpriteRenderer render =  child.gameObject.GetComponent<SpriteRenderer>();
                render.color = invulnerableColor;
                }
            }
            

            
            //Stop player movement
            playerBody.velocity = Vector2.zero;

            //Knockback
            Vector2 newForce;
            newForce.x = Mathf.Sign(transform.rotation.y) * damageRecoil * -1;
            newForce.y = 5.0f;
            playerBody.AddForce(newForce, ForceMode2D.Impulse);

            nextDamageTime = Time.time + damageCooldown;

            health = health - dmg;
            if (health <=0)
            {
                KillPlayer();
            }
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
