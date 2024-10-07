using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Bullet : MonoBehaviour
{
    public float life = 1.5f;
    public int targets = 1;
    private int bulletDamage = 1;
    
    public AudioClip clip;
    public float volume;

    private int targetsHit;

    private void Awake()
    {    
        AudioSource.PlayClipAtPoint(clip, gameObject.transform.localPosition, volume);
        targetsHit = 0;
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
            targetsHit += 1;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
        }

        if(targetsHit >= targets)
        {
            Destroy(gameObject);
        }
                
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targetsHit += 1;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
        }

        if (targetsHit >= targets)
        {
            Destroy(gameObject);
        }
    }
}
