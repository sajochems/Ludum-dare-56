using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public float health = 1000f;

    public void UseHouse()
    {
        GameState.SwitchState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PinkEnemy")
        {
            float damage = collision.gameObject.GetComponent<PinkEnemy>().Attack();
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
                Debug.Log("You lose");
            }
        }
    }
}
