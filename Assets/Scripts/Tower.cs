using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Tower : MonoBehaviour
{

    protected float bulletSpeed;
    protected int bulletDamage;
    protected float attackSpeed;

    public int catFoodCost;
    public int catCost;

    public int CatFoodCost()
    {
        return catFoodCost;
    }

    public int CatCost()
    {
        return catCost;
    }

}
