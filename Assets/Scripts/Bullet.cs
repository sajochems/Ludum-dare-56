using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 1f;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name != "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }    
    }
}
