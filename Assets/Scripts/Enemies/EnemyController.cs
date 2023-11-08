using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyController : MonoBehaviour, Hittable
{
    [SerializeField] private float health = 100;

    public float Health { get => health; set => Health = health; }

    [SerializeField] float damage;

    public void InitEnemy()
    {
        health = 100;
        damage = 3;
    }

    public void TakeDamage(float amount)
    {
        if (health > amount)
        {
            health -= amount;
        }
        else
        {
            health = 0;
            //death
        }
        Debug.Log("Health After: " + health);
    }

    public bool HasZeroHealth()
    {
        return health == 0;
    }

    void Start()
    {
        
    }

    void Update()
    {
        /*if ()*/
    }

}
