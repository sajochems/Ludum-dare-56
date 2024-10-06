using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Enemy : MonoBehaviour
{
    protected float health;
    protected float scoreIncrease;

    protected WaveSpawner waveSpawner;

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

        waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;

        Debug.Log("Enemy killed");
    }

}
