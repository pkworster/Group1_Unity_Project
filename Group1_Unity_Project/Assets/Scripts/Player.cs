using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // attempting class within a class... in the event we want to do more things than a health bar

    [System.Serializable]
    public class PlayerStats
    {
        public float Health = 100;
    }
    public PlayerStats playerStats = new PlayerStats();

    public int fallBoundary = -40;

    int dmg = 25;

    //should add collision damage?????
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            playerStats.Health -= dmg;
    }

    private void Update()
    {
        if (transform.position.y <= fallBoundary)
            DamagePlayer(999999999);
    }


    public void DamagePlayer (int dmg)
    {
        playerStats.Health -= dmg;
        if (playerStats.Health <=0)
        {
            GameMaster.KillPlayer(this);
        }
    }
    private void Start()
    {
        
    }

}



// player health script - Peter Worster
// Test Parts out - death screen tie ins - Peter Worster 11/21
// wiped out... going to try something different 11/22
// 2nd or 3rd attempt 11/28
