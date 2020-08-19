using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public bool projectile6;
    public bool projectile8;
    public GameObject explosion;
    public int damage;

    public GameObject effect6;

    private Transform player;
    private static int weaponUpgrateNum = 0;

    public GameObject soundObject;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("DestoryPorjectile", lifeTime); // callback to function in " " and the delay we calling it
       // old function:  Destroy(gameObject, lifeTime); // destroy the shot after the time pass

        Instantiate(soundObject, transform.position, transform.rotation);
    }
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestoryPorjectile()
    {
        Instantiate(explosion /*what are you spawing */, transform.position /* at what position */, Quaternion.identity /* at what rotation */);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D colliision)
    {
        if (player != null)
        {
            player.GetComponent<Player>().GetWeaponUpgrateNum(ref weaponUpgrateNum);
            Debug.Log("to do:" + weaponUpgrateNum);
            if (colliision.tag == "Enemy")
            {
                colliision.GetComponent<Enemy>().TakeDamage(damage);
                if (projectile6 == false)
                {
                    DestoryPorjectile();
                }
                else
                {
                    Instantiate(effect6, transform.position, Quaternion.identity);
                }
                if (projectile8 == true)
                {
                    colliision.GetComponent<Enemy>().LifeSteal();
                }

            }

            if (colliision.tag == "Boss")
            {
                colliision.GetComponent<Boss>().TakeDamage(damage);
                DestoryPorjectile();
            }
        }
    }
}
