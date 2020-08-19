using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public GameObject projectile2;
    public Transform shotPoint; // the x,y that the shot shooting from
    public float timeBetweenShots; // the time between shots
    private float shotTime; // the game time that it's has been shot
    private float tmpTime = 0;
    private float timeLock = 0;
    public bool weapon2;
    public bool weapon3;
    public bool weapon4;
    public bool weapon5;
    public bool weapon7;
    public bool weapon8;
    public bool weapon9;
    private bool weapon9firstShot = false;

    Animator cameraAnim;
    private void Start()
    {
        cameraAnim = Camera.main.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 direction;
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation;
        Quaternion rotation2 = Quaternion.AngleAxis(0, Vector3.forward);
        Quaternion rotation3 = Quaternion.AngleAxis(0, Vector3.forward);
        Quaternion rotation4 = Quaternion.AngleAxis(0, Vector3.forward);

        if (weapon2 == true)
        {
            rotation = Quaternion.AngleAxis(angle - Random.Range(80, 100), Vector3.forward);
        }
        else
        {
            rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }


        if(weapon3 == true)
        {
            rotation2 = Quaternion.AngleAxis(angle - 70, Vector3.forward);
            rotation3 = Quaternion.AngleAxis(angle - 110, Vector3.forward);
        }
        else if(weapon4 == true)
        {
            rotation2 = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            rotation3 = Quaternion.AngleAxis(angle + 180, Vector3.forward);
            rotation4 = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (weapon5 == true)
        {
            if (tmpTime + 0.5 < Time.time)
            {
                tmpTime = Time.time;
                rotation2 = Quaternion.AngleAxis(angle - Random.Range(0,359), Vector3.forward);
                rotation3 = Quaternion.AngleAxis(angle - Random.Range(0,359), Vector3.forward);
                rotation4 = Quaternion.AngleAxis(angle - Random.Range(0,359), Vector3.forward);
                Instantiate(projectile2, shotPoint.position, rotation2);
                Instantiate(projectile2, shotPoint.position, rotation3);
                Instantiate(projectile2, shotPoint.position, rotation4);
            }
        }
        if (weapon7 == true)
        {
            if (tmpTime + 5 < Time.time)
            {
                tmpTime = Time.time;
                for(int i = 0; i < 50; i++)
                {
                    rotation2 = Quaternion.AngleAxis(angle - Random.Range(0, 359), Vector3.forward);
                    Instantiate(projectile2, shotPoint.position, rotation2);
                }
                cameraAnim.SetTrigger("shake2");
            }
        }

        if(weapon8 == true)
        {
            rotation = Quaternion.AngleAxis(angle - Random.Range(50, 130), Vector3.forward);
        }

        if(weapon9 == true)
        {
            rotation = Quaternion.AngleAxis(angle - Random.Range(-80, -100), Vector3.forward);
            rotation2 = Quaternion.AngleAxis(angle - Random.Range(-80, -100), Vector3.forward);
            rotation3 = Quaternion.AngleAxis(angle - Random.Range(-80, -100), Vector3.forward);
        }

        transform.rotation = rotation;

        if (Input.GetMouseButton(0)) // left click on mouse
        {
            if (weapon9 == true)
            {
                if (weapon9firstShot == false && timeLock < Time.time)
                {
                    weapon9firstShot = true;
                    tmpTime = Time.time + 0.5f;
                }
                else if(weapon9firstShot == true && tmpTime < Time.time)
                {
                    weapon9firstShot = false;
                    timeLock = Time.time + 0.8f;
                }
            }


            if ((Time.time >= shotTime) && (weapon7 == false))
            {
                if (timeLock < Time.time)
                {
                    Instantiate(projectile, shotPoint.position, transform.rotation); //swpan the projectile

                    if (weapon3 == true || weapon9 == true)
                    {
                        Instantiate(projectile, shotPoint.position, rotation2);
                        Instantiate(projectile, shotPoint.position, rotation3);
                    }
                }
                
                if(weapon4 == true)
                {
                    Instantiate(projectile, shotPoint.position, rotation2);
                    Instantiate(projectile, shotPoint.position, rotation3);
                    Instantiate(projectile, shotPoint.position, rotation4);
                }

                cameraAnim.SetTrigger("shake");
                shotTime = Time.time + timeBetweenShots;
            }

        }
    }
}
