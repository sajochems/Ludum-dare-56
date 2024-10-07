using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FireBallTower : Tower
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    private float counter;

    private Queue<Enemy> targets;

    // Start is called before the first frame update
    void Start()
    {
        targets = new Queue<Enemy>();
        bulletSpeed = 2f;
        bulletDamage = 100;
        attackSpeed = 200f;

        catFoodCost = 500;
        catCost = 10;
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

    private void Attack(Enemy target)
    {
        Transform targetLocation = target.GetTransform();
        Vector3 rotation = targetLocation.position - bulletSpawnPoint.position;

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
