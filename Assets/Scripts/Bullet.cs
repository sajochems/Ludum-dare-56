using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Bullet : MonoBehaviour
{
    public float life = 1.5f;
    private int bulletDamage = 1;
    
    public AudioClip clip;

    private void Awake()
    {
        
        AudioSource.PlayClipAtPoint(clip, gameObject.transform.localPosition, 0.25f);
        Destroy(gameObject, life);
    }

    public void SetDamage(int damage)
    {
        bulletDamage = damage; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
        }
        Destroy(gameObject);         
    }
}
