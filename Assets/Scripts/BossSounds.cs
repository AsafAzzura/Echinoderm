using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{

    private AudioSource source;
    public AudioClip[] clips;
    public AudioClip[] clipsStage2;
    public float timeBetweenSoundEffects;
    public float timeBetweenSoundEffects2;
    private float nextSoundEffectTime = 0;
    private bool stage2Init = false;
    private int randomNumber;
    public AudioClip explosion;
    private bool bossInstage2 = false;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (bossInstage2 == false)
        {
            bossInstage2 = GetComponent<Boss>().stage2;
            if (Time.time >= nextSoundEffectTime)
            {
                randomNumber = Random.Range(0, clips.Length);
                source.clip = clips[randomNumber];
                source.Play();
                nextSoundEffectTime = Time.time + timeBetweenSoundEffects;
            }
        }
        else
        {
            if (stage2Init == false)
            {
                stage2Init = true;
                source.Stop();
                source.clip = explosion;
                source.Play();
            }

            if (Time.time >= nextSoundEffectTime)
            {
                randomNumber = Random.Range(0, clipsStage2.Length);
                source.clip = clipsStage2[randomNumber];
                source.Play();
                nextSoundEffectTime = Time.time + timeBetweenSoundEffects2;
            }
        }
    }
}
