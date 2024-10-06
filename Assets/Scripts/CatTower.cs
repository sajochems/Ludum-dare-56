using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTower : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletDamage = 20f;

    public float attackSpeed;

    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter >= attackSpeed)
        {
            Attack();
            counter = 0;
        }
    }

    private void Attack()
    {
        Debug.Log(" I do an attack")
    }
}
