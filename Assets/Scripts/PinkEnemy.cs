using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PinkEnemy : MonoBehaviour
{
    
    public Rigidbody2D rb;

    private Transform target;

    private float health;
    private float speed;
    private float strength;
    private float scoreIncrease;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();



        health = 100f;
        speed = 1f;
        strength = 1f;
        scoreIncrease = 20f;
    }

    private void FixedUpdate()
    {
        //Vector2 playerDirection = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        //rb.velocity = new Vector2(playerDirection.x * speed, playerDirection.y * speed);

        Vector2 homeDirection = new Vector2(0f - transform.position.x, 0f - transform.position.y);
        rb.velocity = new Vector2(homeDirection.x * speed, homeDirection.y * speed);
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

    public float Attack()
    {
        return strength;
    }

    void Die()
    {
        GameState.IncreaseScore(scoreIncrease);
        Destroy(gameObject);
        Debug.Log("Enemy killed");
    }
}
