using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public Enemy[] enemies;
    private float halfHealth;
    private Animator anim;
    public int damage;
    private Slider healthBar;
    private BoxCollider2D boxC2D; //active the box collider only after boss landing
    private float healthToSummon;
    private Vector3 spawnOffest = new Vector3(0, 0, 0);
    Animator cameraAnim;
    public GameObject deathEffect;

    [HideInInspector] 
    public bool stage2 = false;

    // Start is called before the first frame update
    void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        boxC2D = GetComponent<BoxCollider2D>();
        boxC2D.enabled = false;
        healthToSummon = health;
        cameraAnim = Camera.main.GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            healthBar.gameObject.SetActive(false);
        }
        
        if(health <= halfHealth)
        {
            if (stage2 == false)
            {
                stage2 = true;
                cameraAnim.SetTrigger("shake3");
            }
            anim.SetTrigger("stage2");
        }

        if((healthToSummon - (healthToSummon / 10)) > health)
        {
            healthToSummon -= healthToSummon / 10;

            for (int i = 0; i < 4; i++)
            {
                Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];

                if (i == 0)
                {
                    spawnOffest.x += 0.8f;
                    spawnOffest.y += 0.8f;
                }
                else if (i == 1)
                {
                    spawnOffest.x -= 0.8f;
                    spawnOffest.y += 0.8f;
                }
                else if (i == 2)
                {
                    spawnOffest.x -= 0.8f;
                    spawnOffest.y -= 0.8f;

                }
                else if (i == 3)
                {
                    spawnOffest.x += 0.8f;
                    spawnOffest.y -= 0.8f;

                }
                Instantiate(randomEnemy, transform.position + spawnOffest, transform.rotation);
                spawnOffest.x = 0;
                spawnOffest.y = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            collision.GetComponent<Player>().Knockback(10000f, transform.position);
        }
    }

    public void bossLanded()
    {
        boxC2D.enabled = true; // boss laneded, this function was called from the animation
    }
}
