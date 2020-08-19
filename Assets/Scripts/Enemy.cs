using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    [HideInInspector] // this will let starfish to use this public but will bot show in unity
    public Transform player;

    public float speed;
    public float timeBetweenAttack;
    public int damage;

    public int pickupChance;
    public GameObject[] pickups;

    public int healthPickupChance;
    public GameObject healthPickup;

    public GameObject deathEffect;
    public GameObject body;
    private static bool lockDrop = false; // to fix the bug that droping 2+ items

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            if (lockDrop == false)
            {
                lockDrop = true;
                int randomNumber = Random.Range(0, 101);
                if (randomNumber < pickupChance) //drop a weapon
                {
                    GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                    Instantiate(randomPickup, transform.position, transform.rotation);
                }
                else if (randomNumber < pickupChance + healthPickupChance) //drop a heart
                {
                    Instantiate(healthPickup, transform.position, transform.rotation);
                }
            }

            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Instantiate(body, transform.position, Quaternion.identity);
            Destroy(gameObject);
            lockDrop = false;
        }
    }

    public void LifeSteal() // taking dmg my drop some hearts
    {
        int randomNumber = Random.Range(0, 101);
        if (randomNumber < 4) //drop a heart
        {
            Instantiate(healthPickup, transform.position, transform.rotation);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
}
