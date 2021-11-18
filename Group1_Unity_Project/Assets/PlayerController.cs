using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float jumpForce = 30.0f;
    private float jumpTime = 0f;
    [SerializeField] private LayerMask jumpLayer;
    private Rigidbody2D playerBody;
    private BoxCollider2D playerCollider;
    private RaycastHit2D groundedHit;
    private bool justJumped;
    private bool isJumping;
    public float horizontalVel;
    public float verticalVel;
    

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
        horizontalVel = speed * horizontalInput;
        playerBody.velocity = new Vector2(horizontalVel, playerBody.velocity.y);

    }

    // Update is called once per frame
    void Update()
    {
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
    private bool isGrounded() {
        return Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpLayer);
    }    
    
}
