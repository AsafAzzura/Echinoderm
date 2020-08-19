using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    private Animator anim;

    private Rigidbody2D rb; // contain phyisic

    private Vector2 moveAmount;
    public int health;
    private Vector3 weaponBugOffset = new Vector3(0.15f, -0.22f, 0);
    private Vector3 shieldBugOffset = new Vector3(0.15f, 0.5f, 0);
    
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Animator hurtAnim;
    private bool knockbackPlayer = false;
    private float knockAmountPlayer = 0;
    private Vector3 enemyPosKnock = new Vector3(0, 0, 0);
    private float knockTimer = 0;
    private bool shieldOn = false;

    private static int weaponUpgrateNum = 0;
    public GameObject shieldEffect;
    private float shieldTime;

    public int armor;
    public ArmorValueFunc armorFunc;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb   = GetComponent<Rigidbody2D>();
        armorFunc.UpdateArmorValue(armor);
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        if(moveInput != Vector2.zero) // moving
        {
            anim.SetBool("IsRunning", true);
        }
        else //not moving
        {
            anim.SetBool("IsRunning", false);
        }

        if (knockbackPlayer == true)
        {
            if (knockTimer < Time.time)
            {
                knockbackPlayer = false;
            }
        }
        else
        {
            knockTimer = Time.time + 0.25f;
        }

        if(shieldOn == true)
        {
            if (shieldTime < Time.time)
            {
                SetShieldValue(false);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);

        if(knockbackPlayer == true)
        {
            Vector3 dir = transform.position - enemyPosKnock;
            rb.AddForce(dir * knockAmountPlayer * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (shieldOn == false)
        {
            hurtAnim.SetTrigger("hurt");
            if (armor == 0)
            {
                health -= damageAmount;
                updateHealthUI(health);
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else 
            {
                armor -= 1;
                updateArmorUI();
            }
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position + weaponBugOffset, transform.rotation, transform /* this stick it to player */);
    }

    public void PickTurrent(Turrents turrentToEquip)
    {
        Instantiate(turrentToEquip, transform.position + weaponBugOffset , transform.rotation);
    }

    void updateHealthUI(int currentHealth)
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else 
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void updateArmorUI()
    {
        // fix how to call other func null https://answers.unity.com/questions/7555/how-do-i-call-a-function-in-another-gameobjects-sc.html
        armorFunc.UpdateArmorValue(armor);
    }

    public void Heal(int healAmount)
    {
        if(health + healAmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }

        updateHealthUI(health);
    }

    public void Knockback(float knockAmount, Vector3 enemyPos)
    {
        if(knockbackPlayer == false)
        { 
          knockbackPlayer = true;
          knockAmountPlayer = knockAmount;
          enemyPosKnock = enemyPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            knockbackPlayer = false;
        }
    }

    public void SetWeaponUpgrateNum()
    {
        weaponUpgrateNum++;
    }

    public void GetWeaponUpgrateNum(ref int tmpWeaponUpgrateNum)
    {
        tmpWeaponUpgrateNum = weaponUpgrateNum;
    }

    public void SetShieldValue(bool shieldStatus)
    {
        shieldOn = shieldStatus;
        if(shieldOn == true)
        {
            shieldTime = Time.time + 6;
            Instantiate(shieldEffect, transform.position + shieldBugOffset, transform.rotation, transform);
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("Shield"));
        }
    }
}
