using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrents : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint; // the x,y that the shot shooting from
    public float timeBetweenShots; // the time between shots
    private float shotTime; // the game time that it's has been shot
    private Transform player;
    public bool turrents2;
    private bool dontShot = true;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction;
        Quaternion rotation;

        if (turrents2 == false)
        {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = rotation;
        }


        if (player == null)
        {
            Destroy(gameObject);
        }

        if ((Time.time >= shotTime))
        {
            if (turrents2 == true)
            {
                if (dontShot == true)
                {
                    dontShot = false;
                }
                else
                {
                    for (int i = 1; i < 13; i++)
                    {
                        transform.rotation = Quaternion.AngleAxis(30 * i, Vector3.forward);

                        Instantiate(projectile, shotPoint.position, transform.rotation); //swpan the projectil
                    }
                }
            }
            else
            {
                Instantiate(projectile, shotPoint.position, transform.rotation); //swpan the projectil
            }

            shotTime = Time.time + timeBetweenShots;
        }
    }
}
