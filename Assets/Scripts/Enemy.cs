using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Enemy : MonoBehaviour
{
    protected int health;
    protected int scoreIncrease;

    protected WaveSpawner waveSpawner;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameState.IncreaseScore(scoreIncrease);
        GameState.IncreaseCatfood(scoreIncrease);
        Destroy(gameObject);
        GameState.enemiesLeft -= 1;
    }

    public Transform GetTransform()
    {
        return transform;
    }

}
