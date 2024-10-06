using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public float health = 1000f;

    public void UseHouse()
    {
        Debug.Log("Hello house");
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("You lose");
        }

    }
}
