using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Home : MonoBehaviour
{
    public int health = 100;

    public void UseHouse()
    {
        Debug.Log("Hello house");
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
