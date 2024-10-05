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
    private float damage;
    private float scoreIncrease;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        health = 100f;
        speed = 1f;
        damage = 1f;
        scoreIncrease = 20f;
    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
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

    void Die()
    {
        GameState.IncreaseScore(scoreIncrease);
        Destroy(gameObject);
        Debug.Log("Enemy killed");
    }
}
