using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmenyBullet : MonoBehaviour
{
    private Player playerSciript; // to deal dmg to player by calling the take dmg sciript
    private Vector2 targetPosition;
    public float speed;
    public int damage;

    public GameObject destoryBullet;

    // Start is called before the first frame update
    void Start()
    {
        playerSciript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerSciript.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, targetPosition) > .1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            Instantiate(destoryBullet, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerSciript.TakeDamage(damage);
            Instantiate(destoryBullet, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
