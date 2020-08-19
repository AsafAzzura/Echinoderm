using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigStarfishSounds : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] clips;
    public float timeBetweenSoundEffects;
    private float nextSoundEffectTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time >= nextSoundEffectTime)
        {
            int randomNumber = Random.Range(0, clips.Length);
            source.clip = clips[randomNumber];
            source.Play();
            nextSoundEffectTime = Time.time + timeBetweenSoundEffects;
        }
    }
}
