using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfish : Enemy
{
    public float stopDistance;
    private float attackTime;
    public float attackSpeed;
    private AudioSource source;

    private void Update()
    {
        if (player != null) //alive
        {
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); //function to move toward obj 1 to other
            }
            else
            {
                if(Time.time >= attackTime) //attack
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttack;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0; // store how much we done with the animation
        while(percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
