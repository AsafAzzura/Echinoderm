using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTurrents : MonoBehaviour
{
    public Turrents turrentToEquip;
    public GameObject effect;
    private static bool lockPU = false; // to fix the bug that pickup 2+ items

    private void Update()
    {
        if (lockPU == true)
        {
            lockPU = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (lockPU == false)
            {
                lockPU = true;
                Instantiate(effect, transform.position, Quaternion.identity);
                collision.GetComponent<Player>().PickTurrent(turrentToEquip);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
