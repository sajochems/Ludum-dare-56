using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PinkEnemy : MonoBehaviour
{
    
    public Rigidbody2D rb;

    private Transform target;
    private Home home;

    private float health;
    private float speed;
    private float strength;
    private float attackSpeed;
    private float scoreIncrease;

    private bool inRangeOfHome = false;

    private float lastAttack = 0f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        home = GameObject.FindGameObjectWithTag("Home").GetComponent<Home>();

        health = 100f;
        speed = 1f;
        strength = 1f;

        //100f is 1 attack per second
        attackSpeed = 50f;

        scoreIncrease = 20f;
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Enemy health: " + health);
        if (health <= 0)
        {
            Die();
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


    void Die()
    {
        GameState.IncreaseScore(scoreIncrease);
        Destroy(gameObject);
        Debug.Log("Enemy killed");
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
