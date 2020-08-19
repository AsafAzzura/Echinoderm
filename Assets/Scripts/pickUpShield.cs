using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpShield : MonoBehaviour
{
    public GameObject effect;
    Player playerScript;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Destroy(GameObject.FindGameObjectWithTag("Shield"));
            Instantiate(effect, transform.position, Quaternion.identity);
            playerScript.SetShieldValue(true);

            Destroy(gameObject);
        }
    }
}
