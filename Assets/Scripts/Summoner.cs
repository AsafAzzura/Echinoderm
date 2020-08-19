using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 targetPosition; //spwan place
    private Animator anim;

    public float timeBetweenSummons;
    private float summonTime;

    public Enemy enemyToSummon;

    public float attackSpeed;
    public float stopDistance;
    private float attackTime;

    private Vector3 spawnOffest = new Vector3(0,0,0);
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start(); //call start from enemy script
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
     }

    // Update is called once per frame
    private void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, targetPosition) > .5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else 
            {
                anim.SetBool("isRunning", false);
                if(Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                }
            }

            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                if (Time.time >= attackTime) //attack
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttack;
                }
            }
        }
    }

    public void Summon()
    {
        if (player != null)
        {
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    spawnOffest.x += 0.5f;
                }
                else if(i == 1)
                {
                    spawnOffest.x -= 0.5f;
                }
                else if(i == 2)
                {
                    spawnOffest.x += 0.25f;
                    spawnOffest.y += 0.25f;
                }
                else if (i == 3)
                {
                    spawnOffest.x -= 0.25f;
                    spawnOffest.y += 0.25f;
                }
                else if (i == 4)
                {
                    spawnOffest.x -= 0.25f;
                    spawnOffest.y -= 0.25f;
                }
                else if (i == 5)
                {
                    spawnOffest.x += 0.25f;
                    spawnOffest.y -= 0.25f;
                }

                Instantiate(enemyToSummon, transform.position + spawnOffest, transform.rotation);
                spawnOffest.x = 0;
                spawnOffest.y = 0;
            }
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0; // store how much we done with the animation
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
