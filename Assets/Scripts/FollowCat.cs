using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCat : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 2f;
    public int bulletDamage = 20;

    public float attackSpeed = 1000f;

    public float speed;

    private float counter;

    private Queue<Enemy> targets;
    private Transform player;

    void Start()
    {
        targets = new Queue<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 100 / attackSpeed)
        {
            if (targets.Count > 0)
            {
                Enemy target = targets.Peek();
                if (target == null)
                {
                    targets.Dequeue();
                }
                else
                {
                    Attack(target);
                    counter = 0;
                }

            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 playerDirection = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        rb.velocity = new Vector2(playerDirection.x * speed, playerDirection.y * speed);
    }

    private void Attack(Enemy target)
    {
        Transform targetLocation = target.GetTransform();
        Vector3 rotation = targetLocation.position - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        Vector3 bulletDirection = targetLocation.position - bulletSpawnPoint.position;

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(0, 0, rotZ));
        bullet.GetComponent<Bullet>().SetDamage(bulletDamage);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            targets.Enqueue(collision.gameObject.GetComponent<Enemy>());
        }
    }
}
