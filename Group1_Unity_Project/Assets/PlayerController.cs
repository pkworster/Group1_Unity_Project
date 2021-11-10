using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    [SerializeField] private float speed = 100.0f;
    //private float maxSpeed;
    [SerializeField] private float jump = 33.0f;
    private Rigidbody2D playerBody;
    private BoxCollider2D playerCollider;
    private RaycastHit2D groundedHit;
    [SerializeField] private LayerMask jumpLayer;

    //Visual debugging aid, draws a box approximately where the player's collider + boxcast below is.
    private void OnDrawGizmosSelected()
    {   
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(playerCollider.bounds.center, playerCollider.bounds.size);
    }


    // Start is called before the first frame update
    void Start()
    {
        //Grab our player's Rigidbody
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isGrounded());
        //Reset the player's position by pressing 'r'
        if (Input.GetKeyDown("r"))
        {
            transform.position = new Vector2(0,0);
        }

        //Add force up onto the player if they press 'space'
        if (Input.GetKeyDown("space") && isGrounded()) 
        {
            playerBody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);    
        }

        //Apply a negative or positive force based on the input
        horizontalInput = Input.GetAxis("Horizontal");
        playerBody.AddForce(Vector2.right * speed * horizontalInput * Time.deltaTime, ForceMode2D.Impulse);
    }

    //Returns true if the player is on top of something
    private bool isGrounded() {
        return Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpLayer);
    }    
    
}
