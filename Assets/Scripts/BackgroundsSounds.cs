using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundsSounds : MonoBehaviour
{
    private AudioSource source;
    public AudioClip theme;
    public AudioClip themeNonBoss;
    private bool bossTheme = false;
    private Boss boss;
    private GameObject bossTag;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = themeNonBoss;
        source.loop = true;
        source.volume = 0.05f;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossTheme == false)
        {

            /* solving the tagging and the get component is null issues: https://forum.unity.com/threads/getcomponent-returns-null-solved.543930/ */
            bossTag = GameObject.FindGameObjectWithTag("Boss");
            if (bossTag != null)
            {
                boss = bossTag.GetComponent<Boss>();
                if (boss.stage2 == true)
                {
                    bossTheme = true;
                    source.clip = theme;
                    source.loop = true;
                    source.Play();
                }
            }
        }
    }

}
