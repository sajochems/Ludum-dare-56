using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Home : MonoBehaviour
{
    public int health = 100;

    public void UseHouse()
    {
        if (health == 100){
            return;
        }
        GameState.DecreaseCats(1);
        Destroy(GameObject.FindGameObjectWithTag("FollowCat"));
        health += 10;
        if(health > 100)
        {
            health = 100;
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }

    }
}
