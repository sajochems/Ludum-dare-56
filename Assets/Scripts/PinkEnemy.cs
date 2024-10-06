using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

class PinkEnemy : Enemy
{
    
    public Rigidbody2D rb;

    private Transform target;
    private Home home;
    
    private float speed;
    private int strength;
    private float attackSpeed;

    private bool inRangeOfHome = false;

    private float lastAttack = 0f;


    private void Start()
    {
        waveSpawner = GetComponentInParent<WaveSpawner>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        home = GameObject.FindGameObjectWithTag("Home").GetComponent<Home>();

        System.Random rand = new System.Random();
        health = rand.Next(10, 120);
        speed = 0.25f;
        strength = rand.Next(1, 10);

        //100f is 1 attack per second
        attackSpeed = 50f;

        scoreIncrease = (int)((strength + health)/10);
        inRangeOfHome = false;

        lastAttack = 0f;
    }

    private void FixedUpdate()
    {
        //Vector2 playerDirection = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        //rb.velocity = new Vector2(playerDirection.x * speed, playerDirection.y * speed);

        Vector2 homeDirection = new Vector2(0f - transform.position.x, 0f - transform.position.y);
        rb.velocity = new Vector2(homeDirection.x * speed, homeDirection.y * speed);

        if (inRangeOfHome)
        {
            AttackHome();
        }
    }

    public void AttackHome()
    {
        if(Time.time - lastAttack >= 100/attackSpeed)
        {
            lastAttack = Time.time;
            home.takeDamage(strength);
        }     
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Home")
        {
            inRangeOfHome = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Home")
        {
            inRangeOfHome = false;
        }
    }
}
