using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CatTower : Tower
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    private float counter;

    private Queue<Enemy> targets;

    // Start is called before the first frame update
    void Start()
    {
        targets = new Queue<Enemy>();
        bulletSpeed = 4f;
        bulletDamage = 20;
        attackSpeed = 500f;

        catFoodCost = 20;
        catCost = 1;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter >= 100/attackSpeed)
        {
            if(targets.Count > 0)
            {
                Enemy target = targets.Peek();
                if (target == null)
                {
                    targets.Dequeue();
                } else
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
        Vector3 rotation = targetLocation.position - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        Vector3 bulletDirection = targetLocation.position - bulletSpawnPoint.position;

        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, rotZ));
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
