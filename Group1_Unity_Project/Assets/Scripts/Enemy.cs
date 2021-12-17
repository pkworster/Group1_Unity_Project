using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10.0f;
    private float horizontalVelocity;
    public float health;
    public int damagePower;
    public float moveInterval = 4.0f; //Time enemy spends moving forwards
    public float stopInterval = 0.6f; //Time enemy spends stopping
    public float jumpInterval = 2.5f; // Time enemy waits between jumps
    private float jumpTimeEnd;

    public int fallBoundary = -40;

    public bool isReady = false;
    private bool isMoving;
    private bool isJumping;
    public float jumpPower;
    private Rigidbody2D body;
    private CircleCollider2D enemyCollider;
    public Rigidbody2D bullet;

    public Player player;
    public Transform shotPoint;
    public LayerMask jumpLayer;

    private Vector3 defaultScale;
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        enemyCollider = gameObject.GetComponent<CircleCollider2D>();
        defaultScale = transform.localScale;
        jumpTimeEnd = Time.time + jumpInterval;
        StartCoroutine(Move(moveInterval, stopInterval));
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
            } else {
                //If we're not set to move then stop moving
                body.velocity = new Vector2(0f, body.velocity.y);
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
            if(health <= 0) 
            {
                // Add score? Peter Worster - 12/15
                UIManager.instance.killCount++;
                UIManager.instance.UpdateKillCounterUI();
                Kill();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //If we see that the object we collided into has a Player script we call its Damage function
        if(other.collider.gameObject.GetComponent<Player>() != null) {
            Player player = other.gameObject.GetComponent<Player>();
            player.Damage(damagePower); //This can be set to whatever. 
        }
    }


    private IEnumerator Move(float moveTime, float stopTime)
    {
        for(;;) {

            isMoving = true;

            //Set a new direction based on the player
            ChangeDirection();
            yield return new WaitForSeconds(moveTime);
            isMoving = false;
            Shoot();
            yield return new WaitForSeconds(stopTime);
        }
    }

    private void ChangeDirection()
    {
        Vector3 playerPosition = GameMaster.Instance.Player.transform.position;
        float newDirection = playerPosition.x -transform.position.x;
        newDirection = Mathf.Sign(newDirection);
        horizontalVelocity = speed * newDirection * -1;

        if(horizontalVelocity > 0)
        {
            transform.localScale = new Vector3(defaultScale.x * -1, defaultScale.y, defaultScale.z);
        } else if (horizontalVelocity < 0)
        {
            transform.localScale = defaultScale;
        }
    }

    private void Jump()
    {
        body.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
    }

    private void Shoot() 
    {
        Rigidbody2D newBullet = Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);
        float temp = Mathf.Sign(transform.localScale.x) * -1;
        newBullet.velocity = new Vector3(12.0f * temp, 0f, 0f);
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
